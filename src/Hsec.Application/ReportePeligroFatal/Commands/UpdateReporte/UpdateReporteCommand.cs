using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Common.Models;
using Hsec.Application.ReportePeligroFatal.Queries.GetControlesQuery;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.DeletePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;

namespace Hsec.Application.ReportePeligroFatal.Commands.UpdateReporte
{
    public class UpdateReporteCommand : IRequest<Unit>
    {
        //Dto
        public string CodPeligro { get; set; }
        public string CodPosicion { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public int CodPeportePF { get; set; }
        public List<ControlDto> Controles { get; set; }
        public List<PlanVM> Planes { get; set; }

        public class UpdateReporteCommandHandler : IRequestHandler<UpdateReporteCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public UpdateReporteCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateReporteCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    TReportePF reporte = _context.TReportePF.Find(request.CodPeportePF);

                if (reporte == null)
                {
                    reporte = new TReportePF();
                    reporte.CodPelFatal = request.CodPeligro;
                    reporte.CodPosicionGer = request.CodPosicion;
                    reporte.Anio = request.Anio;
                    reporte.Mes = request.Mes;

                    _context.TReportePF.Add(reporte);
                    await _context.SaveChangesAsync(cancellationToken);

                    foreach (var control in request.Controles)
                    {
                        TReportePFDetalle detalle = new TReportePFDetalle();
                        detalle.CodReportePF = reporte.CodReportePF;
                        detalle.CodCC = control.CodCC;
                        detalle.Observacion = control.Observacion;
                        _context.TReportePFDetalle.Add(detalle);
                    }

                    // var r1 = await _planAccion.Create(request.Planes, reporte.CodReportePF.ToString());
                    request.Planes.ForEach(t => { t.docReferencia = reporte.CodReportePF.ToString(); t.docSubReferencia = reporte.CodReportePF.ToString(); });
                    var r1 = await _mediator.Send(new CreatePlanAccionCommand() { planes = request.Planes });
                    }
                else
                {

                    
                    foreach (var control in request.Controles)
                    {
                        TReportePFDetalle detalle = _context.TReportePFDetalle.Find(control.CodReportePF, control.CodCC);
                        if(detalle != null)
                            {
                                detalle.Observacion = control.Observacion;
                                _context.TReportePFDetalle.Update(detalle);
                            }
                        else
                            {
                                if(control.CodReportePF == 0)
                                {
                                    TReportePFDetalle _detalle = new TReportePFDetalle();
                                    _detalle.CodReportePF = reporte.CodReportePF;
                                    _detalle.CodCC = control.CodCC;
                                    _detalle.Observacion = control.Observacion;
                                    _context.TReportePFDetalle.Add(_detalle);
                                }
                            }    
                    }

                    var updateList = new List<PlanVM> ();
                    var createList = new List<PlanVM> ();
                    foreach (var item in request.Planes) {
                        if (item.estado == false && item.codAccion > 0) {
                                //var r2 = await _planAccion.Delete (item.codAccion.ToString ());
                            var r2 = await _mediator.Send(new DeletePlanAccionCommand() { CodAccion = item.codAccion });
                        } else if (item.estado == true && item.codAccion > 0) {
                            updateList.Add (item);
                        } else if (item.codAccion < 0) {
                            createList.Add (item);
                        }
                    }
                        //var r3 = await _planAccion.Create (createList, request.CodPeportePF.ToString());
                        createList.ForEach(t => { t.docReferencia = request.CodPeportePF.ToString(); t.docSubReferencia = request.CodPeportePF.ToString(); });
                        var r3 = await _mediator.Send(new CreatePlanAccionCommand() { planes = createList });

                        //var r4 = await _planAccion.Update(updateList);
                        var r4 = await _mediator.Send(new UpdatePlanAccionCommand() { planes = updateList });

                    }
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;

                }
                catch (Exception e)
                {
                    Exception ee = e;
                    throw e;
                }
            }
        }
    }
}

