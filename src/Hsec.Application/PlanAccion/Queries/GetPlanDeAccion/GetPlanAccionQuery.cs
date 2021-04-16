using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiasPersonaAll;
using Hsec.Application.General.Jerarquias.Queries.GetJerarquiasPersonaAll;
using Hsec.Application.Common.Models;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class GetPlanAccionQuery : IRequest<PlanAccionVM>
    {
        public GetPlanAccionFiltradoDto plan { get; set; }
        public class GetPlanAccionQueryHandler : IRequestHandler<GetPlanAccionQuery, PlanAccionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetPlanAccionQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<PlanAccionVM> Handle(GetPlanAccionQuery request, CancellationToken cancellationToken)
            {
                DateTime date = DateTime.Today;
                GetPlanAccionFiltradoDto item;
                item = new GetPlanAccionFiltradoDto();
                item = request.plan;
                PlanAccionVM filtro = new PlanAccionVM();
                List<JerarquiaPersonaCodPosicionDto> personasFiltro = new List<JerarquiaPersonaCodPosicionDto>();
                List<string> personas = new List<string>();

                //eta linea sirve para el codPersonaForm cuando llega informacion el boton buscar 
                if(!String.IsNullOrEmpty(request.plan.codPersonaForm)){
                    if (!String.IsNullOrEmpty(item.codPersona)) {
                        personas.Add(item.codPersona);
                    }
                }
                else if (request.plan.codPosicion != null)
                {
                    var codPosicion = request.plan.codPosicion;
                    //llamar servicio de personas x posicion
                    //JerarquiaPersonaCodPosicionVM perPorPosicion = await _perPorPosicion.requestPerJerAccion(codPosicion);
                    GeneralCollection<PersonaPosVM> perPorPosicion = await _mediator.Send(new GetJerarquiasPersonaAllQuery() { CodPosicion = codPosicion.Value });
                    foreach (var it in perPorPosicion.Data) {
                        personas.Add(it.CodPersona);
                    }
                }
                else {
                    if (!String.IsNullOrEmpty(item.codPersona)) {
                        personas.Add(item.codPersona);
                    }
                }
                
                //if (!String.IsNullOrEmpty(item.codPersona))
                if (personas.Count() > 0)
                {
                    //from p in context.ParentTable
                    //join c in context.ChildTable on p.ParentId equals c.ChildParentId into j1
                    //from j2 in j1.DefaultIfEmpty()
                    //group j2 by p.ParentId into grouped
                    //select new { ParentId = grouped.Key, Count = grouped.Count(t => t.ChildId != null) }

                    //var tuplaParcial = tuplaOfi.GroupBy(i => i.Item2).Select(o => o.First()).ToList();
                    var count = ((from accion in _context.TAccion
                                  join responsable in _context.TResponsable on accion.CodAccion equals responsable.CodAccion into lista
                                  from list in lista.DefaultIfEmpty()
                                      // group list by accion.CodAccion
                                  where personas.Contains(list.CodPersona) && accion.Estado && ((item.codAccion == 0) || (accion.CodAccion.ToString().EndsWith(item.codAccion.ToString()))) &&
                                   (String.IsNullOrEmpty(item.docReferencia) || accion.DocReferencia.Contains(item.docReferencia)) &&
                                   (String.IsNullOrEmpty(item.codTablaRef) || accion.CodTablaRef.Contains(item.codTablaRef)) &&
                                   (String.IsNullOrEmpty(item.codEstadoAccion) || accion.CodEstadoAccion.Contains(item.codEstadoAccion)) &&
                                   (String.IsNullOrEmpty(item.codEstadoApro) || accion.EstadoAprobacion.Contains(item.codEstadoApro)) &&
                                   ((item.fechaInicial.Date <= accion.FechaSolicitud.Date && accion.FechaSolicitud.Date <= item.fechaFinal.Date))
                                  select accion.CodAccion
                                 )).ToList().GroupBy(i => i).Select(o=>o.First()).Count();

                    var LISTA = (from accion in _context.TAccion
                                 join responsable in _context.TResponsable on accion.CodAccion equals responsable.CodAccion into lista
                                 from list in lista.DefaultIfEmpty()
                                 where personas.Contains(list.CodPersona) && accion.Estado && ((item.codAccion == 0) || (accion.CodAccion.ToString().EndsWith(item.codAccion.ToString()))) &&
                                  (String.IsNullOrEmpty(item.docReferencia) || accion.DocReferencia.Contains(item.docReferencia)) &&
                                  (String.IsNullOrEmpty(item.codTablaRef) || accion.CodTablaRef.Contains(item.codTablaRef)) &&
                                  (String.IsNullOrEmpty(item.codEstadoAccion) || accion.CodEstadoAccion.Contains(item.codEstadoAccion)) &&
                                  (String.IsNullOrEmpty(item.codEstadoApro) || accion.EstadoAprobacion.Contains(item.codEstadoApro)) &&
                                  ((item.fechaInicial.Date <= accion.FechaSolicitud.Date && accion.FechaSolicitud.Date <= item.fechaFinal.Date))
                                 select new PlanVM
                                 {
                                     tarea = accion.Tarea,
                                     fechaInicial = accion.FechaInicial,
                                     fechaFinal = accion.FechaFinal,
                                     estado = accion.Estado,
                                     fechaSolicitud = accion.FechaSolicitud,
                                     docReferencia = accion.DocReferencia,
                                     codAccion = accion.CodAccion,
                                     docSubReferencia = accion.DocSubReferencia,
                                     codTablaRef = accion.CodTablaRef,
                                     codSolicitadoPor = accion.CodSolicitadoPor,
                                     codActiRelacionada = accion.CodActiRelacionada,
                                     codEstadoAccion = accion.CodEstadoAccion,
                                     codAreaHsec = accion.CodAreaHsec,
                                     codTipoAccion = accion.CodTipoAccion,
                                     codNivelRiesgo = accion.CodNivelRiesgo,
                                     Aprobador = accion.Aprobador,
                                     EstadoAprobacion = accion.EstadoAprobacion
                                 }).ToList().GroupBy(i => i.codAccion).Select(o => o.First()).Skip(item.pagina * item.tamanio - item.tamanio).Take(item.tamanio).OrderByDescending(i => i.codAccion);



                    filtro.count =  count;
                    filtro.data = LISTA.ToList();
                }
                else
                {
                    var Count = _context.TAccion.Count((i => i.Estado &&
                   ((item.codAccion == 0) || (i.CodAccion.ToString().EndsWith(item.codAccion.ToString()))) &&
                   (String.IsNullOrEmpty(item.docReferencia) || i.DocReferencia.Contains(item.docReferencia)) &&
                   (String.IsNullOrEmpty(item.codTablaRef) || i.CodTablaRef.Contains(item.codTablaRef)) &&
                   (String.IsNullOrEmpty(item.codEstadoAccion) || i.CodEstadoAccion.Contains(item.codEstadoAccion)) &&
                   (String.IsNullOrEmpty(item.codEstadoApro) || i.EstadoAprobacion.Contains(item.codEstadoApro)) &&
                   ((item.fechaInicial.Date <= i.FechaSolicitud.Date && i.FechaSolicitud.Date <= item.fechaFinal.Date)) &&
                   (String.IsNullOrEmpty(item.codSolicitadoPor) || i.CodSolicitadoPor.Contains(item.codSolicitadoPor))));
;

                    var acc = _context.TAccion.Where((i => i.Estado &&
                    ((item.codAccion == 0) || (i.CodAccion.ToString().EndsWith(item.codAccion.ToString()))) &&
                    (String.IsNullOrEmpty(item.docReferencia) || i.DocReferencia.Contains(item.docReferencia)) &&
                    (String.IsNullOrEmpty(item.codTablaRef) || i.CodTablaRef.Contains(item.codTablaRef)) &&
                    (String.IsNullOrEmpty(item.codEstadoAccion) || i.CodEstadoAccion.Contains(item.codEstadoAccion)) &&
                    (String.IsNullOrEmpty(item.codEstadoApro) || i.EstadoAprobacion.Contains(item.codEstadoApro)) &&
                    ((item.fechaInicial.Date <= i.FechaSolicitud.Date && i.FechaSolicitud.Date <= item.fechaFinal.Date)) &&
                    (String.IsNullOrEmpty(item.codSolicitadoPor) || i.CodSolicitadoPor.Contains(item.codSolicitadoPor))))
                        .OrderByDescending(p=>p.CodAccion)
                        .Skip(item.pagina * item.tamanio - item.tamanio)
                        .Take(item.tamanio);

                    //IList<PlanAccionMaestroDto> plan = new List<PlanAccionMaestroDto>();
                    IList<PlanVM> plan = new List<PlanVM>();

                    //PlanAccionMaestroDto planVM;
                    PlanVM planVM;

                    foreach (var pl in acc)
                    {
                        planVM = new PlanVM();
                        planVM.codAccion = pl.CodAccion;
                        planVM.codEstadoAccion = pl.CodEstadoAccion;
                        planVM.EstadoAprobacion = pl.EstadoAprobacion;
                        planVM.codAreaHsec = pl.CodAreaHsec;
                        planVM.codTipoAccion = pl.CodTipoAccion;
                        planVM.fechaSolicitud = pl.FechaSolicitud;
                        planVM.codActiRelacionada = pl.CodActiRelacionada;
                        planVM.codSolicitadoPor = pl.CodSolicitadoPor;
                        planVM.docReferencia = pl.DocReferencia;
                        planVM.docSubReferencia = pl.DocSubReferencia;
                        planVM.codNivelRiesgo = pl.CodNivelRiesgo;
                        planVM.tarea = pl.Tarea;
                        planVM.fechaInicial = pl.FechaInicial;
                        planVM.fechaFinal = pl.FechaFinal;
                        planVM.estado = pl.Estado;
                        planVM.codTablaRef = pl.CodTablaRef;

                        plan.Add(planVM);
                    }
                    filtro.count =Count;
                    filtro.data = plan;
                }
                return filtro;
            }
        }
    }
}
