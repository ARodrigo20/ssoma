using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Aprobaciones.Commands.AprobacionPlanCreate;

namespace Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion
{
    public class CreatePlanAccionCommand : IRequest<Unit>
    {
        public IList<PlanVM> planes { get; set; }

        public class CreatePlanAccionCommandHandler : IRequestHandler<CreatePlanAccionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            public CreatePlanAccionCommandHandler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(CreatePlanAccionCommand request, CancellationToken cancellationToken)
            {
                //var data = await _context.TTipoAccion.FirstOrDefaultAsync(i => i.CodTipoAccion == request.CodTipoAccion);//.Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/
                DateTime date = DateTime.Today;
                IList<PlanVM> planesAccion = request.planes;
                TAccion data;
                foreach (var plan in planesAccion)
                {

                    bool validador = !String.IsNullOrEmpty(plan.docReferencia) &&
                        !String.IsNullOrEmpty(plan.docSubReferencia) &&
                        !String.IsNullOrEmpty(plan.tarea) &&
                        !String.IsNullOrEmpty(plan.codSolicitadoPor);

                    if (validador)
                    {
                        data = new TAccion();
                        //data.CodEstadoAccion = plan.codEstadoAccion;
                        data.CodAreaHsec = plan.codAreaHsec;
                        data.CodTipoAccion = plan.codTipoAccion;
                        data.FechaSolicitud = plan.fechaSolicitud;
                        data.CodActiRelacionada = plan.codActiRelacionada;
                        data.CodSolicitadoPor = plan.codSolicitadoPor;
                        data.DocReferencia = plan.docReferencia;

                        data.DocSubReferencia = plan.docSubReferencia;
                        data.CodNivelRiesgo = plan.codNivelRiesgo;
                        data.Tarea = plan.tarea;
                        data.FechaInicial = plan.fechaInicial;
                        data.FechaFinal = plan.fechaFinal;
                        data.Estado = true;

                        data.CodTablaRef = plan.codTablaRef;
                        data.CodEstadoAccion = "01";
                        data.Aprobador = plan.Aprobador;
                        data.EstadoAprobacion = "P";

                        data.CreadoPor = "admin";
                        data.Creado = date;
                        data.ModificadoPor = "admin";
                        data.Modificado = date;

                        TResponsable resp;

                        var responsables = plan.RespPlanAccion;
                        foreach (var it in responsables)
                        {
                            resp = new TResponsable();
                            resp.CodPersona = it.codPersona;
                            data.RespPlanAccion.Add(resp);

                        }

                        _context.TAccion.Add(data);
                        await _context.SaveChangesAsync(cancellationToken);

                        //var r1 = await _persons.CreateAprobacionPlan(data.CodAccion, data.DocReferencia, data.CodTablaRef);
                        var r1 = await _mediator.Send(new AprobacionPlanCreateCommand() { CodAccion = data.CodAccion, DocReferencia = data.DocReferencia, CodTabla = data.CodTablaRef });

                        int verificador = _context.TAccion.Count();
                        var ultCodCorrelativo = 1;
                        if (verificador > 0)
                        {
                            ultCodCorrelativo = _context.TAccion.Max(p => p.CodAccion);
                        }
                        //TLevantamientoPlan lev;
                        //foreach (var it in responsables)
                        //{
                        //    lev = new TLevantamientoPlan();
                        //    lev.CodAccion = ultCodCorrelativo;
                        //    lev.CodPersona = it.codPersona;
                        //    lev.PorcentajeAvance = 0;
                        //    lev.Estado = true;
                        //    _context.TLevantamientoPlan.Add(lev);
                        //}
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    else
                    {
                        new GeneralFailureException("Verifique los datos ingresados .. ojo: no deben ser vacios !");
                    }
                }
                return Unit.Value;
            }
        }
    }
}