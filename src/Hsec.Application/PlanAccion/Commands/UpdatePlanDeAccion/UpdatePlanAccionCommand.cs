using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.PlanAccion.Queries.GetPlanDeAccion;

namespace Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion
{
    public partial class UpdatePlanAccionCommand : IRequest<Unit>
    {
        //Codigos de las tablas relacionadas con TAccion son 5 
        public IList<PlanVM> planes { get; set; }

        public class UpdatePlanAccionCommandHandler : IRequestHandler<UpdatePlanAccionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdatePlanAccionCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdatePlanAccionCommand request, CancellationToken cancellationToken)
            {
                IList<PlanVM> planesAccion = new List<PlanVM>();
                planesAccion = request.planes;

                //var planAccion = planesAccion.AsQueryable()
                //   .ProjectTo<TAccion>(_mapper.ConfigurationProvider)
                //   .ToList();

                IList<ResponsablesDto> respJson;

                foreach (var accionJson in planesAccion)
                {
                    var dataBD = await _context.TAccion.Include(i => i.RespPlanAccion).FirstOrDefaultAsync(i => i.CodAccion == accionJson.codAccion && i.Estado);
                    if (dataBD != null)
                    {
                        dataBD.CodActiRelacionada = accionJson.codActiRelacionada;
                        dataBD.CodAreaHsec = accionJson.codAreaHsec;
                        dataBD.FechaSolicitud = accionJson.fechaSolicitud;
                        dataBD.CodActiRelacionada = accionJson.codActiRelacionada;
                        dataBD.CodSolicitadoPor = accionJson.codSolicitadoPor;
                        dataBD.DocReferencia = accionJson.docReferencia;
                        dataBD.DocSubReferencia = accionJson.docSubReferencia;
                        dataBD.CodNivelRiesgo = accionJson.codNivelRiesgo;
                        dataBD.Tarea = accionJson.tarea;
                        dataBD.FechaInicial = accionJson.fechaInicial;
                        dataBD.FechaFinal = accionJson.fechaFinal;
                        dataBD.Estado = accionJson.estado;
                        dataBD.Aprobador = accionJson.Aprobador;

                        respJson = new List<ResponsablesDto>();
                        respJson = accionJson.RespPlanAccion.ToList();
                        var respBD = dataBD.RespPlanAccion.ToList();
                        var inter = respJson.Select(x => x.codPersona).Intersect(respBD.Select(x => x.CodPersona)).ToList();
                        var right = respJson.Select(x => x.codPersona).Except(respBD.Select(x => x.CodPersona)).ToList();
                        var left = respBD.Select(x => x.CodPersona).Except(respJson.Select(x => x.codPersona)).ToList();

                        foreach (var item in left)
                        {
                            var eliminar = dataBD.RespPlanAccion.FirstOrDefault(i => i.CodPersona == item && i.CodAccion == dataBD.CodAccion && i.Estado);
                            var eliminarLevS = _context.TLevantamientoPlan.Where(i => i.CodPersona == item && i.CodAccion == dataBD.CodAccion && i.Estado);
                            if (eliminarLevS.Count() > 0)
                            {
                                foreach (var it in eliminarLevS)
                                {
                                    it.Estado = false;
                                    _context.TLevantamientoPlan.Update(it);
                                }
                            }
                            if (eliminar != null)
                            {
                                eliminar.Estado = false;
                                _context.TResponsable.Update(eliminar);
                            }
                        }
                        TResponsable responsable;
                        foreach (var item in right)
                        {
                            responsable = new TResponsable();
                            var agregar = accionJson.RespPlanAccion.FirstOrDefault(i => i.codPersona == item && i.codAccion == dataBD.CodAccion);
                            responsable.CodAccion = agregar.codAccion;
                            responsable.CodPersona = agregar.codPersona;
                            responsable.Estado = true;
                            if (responsable.CodAccion != 0 && responsable.CodPersona != null)
                            {
                                TLevantamientoPlan lev;
                                lev = new TLevantamientoPlan();
                                lev.CodAccion = responsable.CodAccion;
                                lev.CodPersona = responsable.CodPersona;
                                lev.PorcentajeAvance = 0;
                                lev.Estado = false;
                                _context.TResponsable.Add(responsable);
                                _context.TLevantamientoPlan.Add(lev);
                            }
                        }
                        foreach (var it in inter)
                        {
                            var actualizar = _context.TResponsable.FirstOrDefault(i => i.CodPersona == it && i.CodAccion == dataBD.CodAccion);
                            if (actualizar != null)
                            {
                                actualizar.Estado = true;
                                _context.TResponsable.Update(actualizar);
                            }
                        }
                        _context.TAccion.Update(dataBD);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }

                // Actualiza codEstadoAccion de plan de accion dataBD
                foreach (var accion in planesAccion)
                {
                    var dataBD = _context.TAccion.Include(i => i.RespPlanAccion).FirstOrDefault(i => i.CodAccion == accion.codAccion && i.Estado);//.Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                    if (dataBD != null)
                    {
                        double suma = 0;
                        int count = 0;

                        foreach (var it in dataBD.RespPlanAccion)
                        {
                            if (it.Estado) {
                                var levTarea = _context.TLevantamientoPlan.Where(I => I.CodAccion == accion.codAccion && I.CodPersona == it.CodPersona && I.Estado);
                                if (levTarea.Count() > 0)
                                {
                                    var maxLevTarea = levTarea.Max(I => I.PorcentajeAvance);
                                    suma += maxLevTarea;
                                    count++;
                                }
                            }
                        }

                        double sumaDiv = 0;
                        if (count > 0)
                        {
                            sumaDiv = (suma / count);
                        }

                        if (sumaDiv > 0 && sumaDiv < 100)
                        {
                            dataBD.CodEstadoAccion = "03";
                        }

                        else if (sumaDiv >= 100)
                        {
                            dataBD.CodEstadoAccion = "02";
                        }

                        else
                        {
                            dataBD.CodEstadoAccion = "01";
                        }
                        _context.TAccion.Update(dataBD);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
                return Unit.Value;
            }
        }
    }
}

