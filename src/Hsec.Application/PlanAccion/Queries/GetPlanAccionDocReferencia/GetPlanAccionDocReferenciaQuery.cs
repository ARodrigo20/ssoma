using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.PlanAccion.Queries.GetPlanDeAccion;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;
using Hsec.Application.General.Personas.Queries.GetCode2Name;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.PlanAccion.Queries.GetPlanAccionDocReferencia
{
    public class GetPlanAccionDocReferenciaQuery : IRequest<PlanAccionVM>
    {
        public string DocReferencia { get; set; }
        public string DocSubReferencia { get; set; }
        public string TablaReferencia { get; set; }

        public class GetPlanAccionDocReferenciaQueryHandler : IRequestHandler<GetPlanAccionDocReferenciaQuery, PlanAccionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            public GetPlanAccionDocReferenciaQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<PlanAccionVM> Handle(GetPlanAccionDocReferenciaQuery request, CancellationToken cancellationToken)
            {
                PlanAccionVM planes = new PlanAccionVM();

                var result = _context.TAccion.Include(i => i.RespPlanAccion).Where(i => i.DocReferencia == request.DocReferencia && (string.IsNullOrEmpty(request.DocSubReferencia) || i.DocSubReferencia.Equals(request.DocSubReferencia)) && (string.IsNullOrEmpty(request.TablaReferencia) || i.CodTablaRef.Equals(request.TablaReferencia))).Where(i => i.Estado);//.Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                //PlanAccionMaestroDto acciones;
                PlanVM acciones;


                foreach (var item in result)
                {
                    acciones = new PlanVM();
                    ResponsablesDto accionRes;
                    int count = 0;

                    foreach (var it in item.RespPlanAccion)
                    {
                        if (it.Estado)
                        {
                            accionRes = new ResponsablesDto();
                            accionRes.codAccion = it.CodAccion;
                            accionRes.codPersona = it.CodPersona;
                            accionRes.estado = it.Estado;
                            PersonaVM respVM = new PersonaVM();
                            //respVM = await _persons.RequestNombApellidos(it.CodPersona);
                            respVM = await _mediator.Send(new GetPersonaQuery() { CodPersona = it.CodPersona });
                            if (respVM != null)
                            {
                                accionRes.nombres = respVM.Nombres;
                                accionRes.apellidoPaterno = respVM.ApellidoPaterno;
                                accionRes.apellidoMaterno = respVM.ApellidoMaterno;
                            }
                            else
                            {
                                accionRes.nombres = "no se encontro dicha persona !!";
                            }
                            var _porcentajes = _context.TLevantamientoPlan.Where(i => i.CodAccion == it.CodAccion && i.CodPersona == it.CodPersona);
                            if(_porcentajes.Count() > 0)
                            {
                                accionRes.porcentajeMayor = _porcentajes.Max(i => i.PorcentajeAvance);
                            }
                            //accionRes.porcentajeMayor = _context.TLevantamientoPlan.Where(i => i.CodAccion == it.CodAccion && i.CodPersona == it.CodPersona).Max(i => i.PorcentajeAvance);

                            acciones.RespPlanAccion.Add(accionRes);
                            count++;
                        }
                    }

                    acciones.codAccion = item.CodAccion;
                    //acciones.codEstadoAccion = item.CodEstadoAccion;
                    acciones.codAreaHsec = item.CodAreaHsec;
                    acciones.codTipoAccion = item.CodTipoAccion;
                    acciones.fechaSolicitud = item.FechaSolicitud;
                    acciones.codActiRelacionada = item.CodActiRelacionada;
                    acciones.codSolicitadoPor = item.CodSolicitadoPor;

                    List<String> solPers = new List<String>();
                    solPers.Add(item.CodSolicitadoPor);
                    acciones.nombreSolicitadoPor = "no se encontro dicha persona !!";
                    //solPers = await _persons.Request(solPers);
                    solPers = await _mediator.Send(new GetCode2NameQuery() { data = solPers });
                    if (solPers.Count() > 0)
                    {
                        acciones.nombreSolicitadoPor = solPers[0];
                    }

                    acciones.docReferencia = item.DocReferencia;

                    acciones.docSubReferencia = item.DocSubReferencia;
                    acciones.codNivelRiesgo = item.CodNivelRiesgo;
                    acciones.tarea = item.Tarea;
                    acciones.fechaInicial = item.FechaInicial;
                    acciones.fechaFinal = item.FechaFinal;
                    acciones.estado = item.Estado;

                    acciones.codTablaRef = item.CodTablaRef;
                    acciones.codEstadoAccion = item.CodEstadoAccion;
                    acciones.Aprobador = item.Aprobador;
                    acciones.EstadoAprobacion = item.EstadoAprobacion;
                    planes.count = acciones.RespPlanAccion.Count();
                    planes.data.Add(acciones);
                }
                return planes;
            }
        }
    }
}
