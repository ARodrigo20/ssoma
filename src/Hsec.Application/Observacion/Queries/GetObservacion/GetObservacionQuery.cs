using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Cartilla.Queries.GetCartilla;

namespace Hsec.Application.Observacion.Queries.GetObservacion
{
    public class GetObservacionQuery : IRequest<ObservacionVM>
    {
        public string CodObservacion { get; set; }
        public class GetObservacionQueryHandler : IRequestHandler<GetObservacionQuery, ObservacionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetObservacionQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<ObservacionVM> Handle(GetObservacionQuery request, CancellationToken cancellationToken)
            {
                TObservacion obs = _context.TObservaciones
                    .FirstOrDefault(o => o.Estado == true && o.CodObservacion.Equals(request.CodObservacion));

                if (obs == null) throw new NotFoundException("observacion", request.CodObservacion);

                string COD_OBSERVACION = obs.CodObservacion;
                ObservacionVM obsVM = _mapper.Map<TObservacion, ObservacionVM>(obs);

                obsVM.CodTipoObservacion = (obsVM.CodSubTipoObs != null && obsVM.CodSubTipoObs != "") ? obsVM.CodTipoObservacion + "." + obsVM.CodSubTipoObs : obsVM.CodTipoObservacion;

                if (obsVM.CodTipoObservacion == "3") //obs.CodTipoObservacion == TipoObservacion.Tarea
                {
                    TObservacionTarea obs_tarea = _context.TObservacionTareas
                        .First(ocon => ocon.CodObservacion.Equals(COD_OBSERVACION));
                    obsVM.Tarea = _mapper.Map<TObservacionTarea, TareaDto>(obs_tarea);

                    obsVM.Tarea.RegistroEncuestas = _context.TObsTaRegistroEncuestas
                        .Where(t => t.CodObservacion.Equals(COD_OBSERVACION))
                        .ProjectTo<RegistroEncuestaDto>(_mapper.ConfigurationProvider)
                        .ToList();
                    obsVM.Tarea.EtapaTareas = _context.TObsTaEtapaTareas
                        .Where(t => t.CodObservacion.Equals(COD_OBSERVACION))
                        .ProjectTo<EtapaTareaDto>(_mapper.ConfigurationProvider)
                        .ToList();
                    obsVM.Tarea.Comentarios = _context.TObsTaComentarios
                        .Where(t => t.CodObservacion.Equals(COD_OBSERVACION))
                        .ProjectTo<ComentarioDto>(_mapper.ConfigurationProvider)
                        .ToList();
                    obsVM.Tarea.PersonaObservadas = _context.TObsTaPersonaObservadas
                        .Where(t => t.CodObservacion.Equals(COD_OBSERVACION))
                        .Select(t => t.CodPersonaMiembro)
                        .ToList();
                    
                }
                if (obsVM.CodTipoObservacion == "4.1") //obs.CodTipoObservacion == TipoObservacion.Iteraccion_Seguridad
                {
                    TObservacionIteraccion obs_iteraccion = _context.TObservacionIteracciones
                        .First(ocon => ocon.CodObservacion.Equals(obs.CodObservacion));
                    obsVM.IteraccionSeguridad = _mapper.Map<TObservacionIteraccion, IteraccionSeguridadDto>(obs_iteraccion);

                    IList<TObsISRegistroEncuesta> list = _context
                        .TObsISRegistroEncuestas
                        .Where(orem => orem.CodObservacion.Equals(COD_OBSERVACION))
                        .ToList();

                    IList<TObsISRegistroEncuesta> listTemp;
                    listTemp = list.Where(t => t.CodEncuesta.Equals(TipoEncuestaIteraccion.ActividadAltoRiesgo.GetHashCode().ToString())).ToList();
                    foreach (var obj in listTemp)
                    {
                        obsVM.IteraccionSeguridad.ActividadAltoRiesgo.Add(obj.CodDescripcion.ToString());
                    }
                    listTemp = list.Where(t => t.CodEncuesta.Equals(TipoEncuestaIteraccion.ClasificacionObservacion.GetHashCode().ToString())).ToList();
                    foreach (var obj in listTemp)
                    {
                        obsVM.IteraccionSeguridad.ClasificacionObservacion.Add(obj.CodDescripcion.ToString());
                    }
                    listTemp = list.Where(t => t.CodEncuesta.Equals(TipoEncuestaIteraccion.ComportamientoRiesgoCondicion.GetHashCode().ToString())).ToList();
                    foreach (var obj in listTemp)
                    {
                        obsVM.IteraccionSeguridad.ComportamientoRiesgoCondicion.Add(obj.CodDescripcion.ToString());
                    }
                    listTemp = list.Where(t => t.CodEncuesta.Equals(TipoEncuestaIteraccion.MetodologiaGestionRiesgos.GetHashCode().ToString())).ToList();
                    foreach (var obj in listTemp)
                    {
                        obsVM.IteraccionSeguridad.MetodologiaGestionRiesgos.Add(obj.CodDescripcion.ToString());
                    }

                }
                if (obsVM.CodTipoObservacion == "1") //obs.CodTipoObservacion == TipoObservacion.Comportamiento
                {
                    TObservacionComportamiento obs_comportamiento = _context.TObservacionComportamientos
                        .First(ocon => ocon.CodObservacion.Equals(obs.CodObservacion));
                    obsVM.Comportamiento = _mapper.Map<TObservacionComportamiento, ComportamientoDto>(obs_comportamiento);
                }
                if (obsVM.CodTipoObservacion == "2") //obs.CodTipoObservacion == TipoObservacion.Condicion
                {
                    TObservacionCondicion obs_condicion = _context.TObservacionCondiciones
                        .First(ocon => ocon.CodObservacion.Equals(obs.CodObservacion));
                    obsVM.Condicion = _mapper.Map<TObservacionCondicion, CondicionDto>(obs_condicion);
                }
                if (obsVM.CodTipoObservacion == "4.2" || obsVM.CodTipoObservacion == "4.3") //obs.CodTipoObservacion == TipoObservacion.VerificacionControlCritico || obs.CodTipoObservacion == TipoObservacion.Covid19
                {
                    TObservacionVerControlCritico obs_vcc = _context.TObservacionVerControlCritico
                        .First(ocon => ocon.CodObservacion.Equals(obs.CodObservacion));
                    var verControlCritico = _mapper.Map<TObservacionVerControlCritico, VerificacionControlCriticoDto>(obs_vcc);
                    verControlCritico.Herramientas = _context.TObsVCCHerramienta
                                                                            .Where(t => t.CodVcc.Equals(obs_vcc.CodVcc))
                                                                            .Select(t => t.CodDesHe)
                                                                            .ToHashSet();
                    verControlCritico.Criterios = _context.TObsVCCRespuesta
                                                                            .Where(t => t.CodVcc.Equals(obs_vcc.CodVcc))
                                                                            .ProjectTo<CriterioDto>(_mapper.ConfigurationProvider)
                                                                            .OrderBy(t => t.CodCrit)
                                                                            .ToHashSet()
                                                                            ;
                    verControlCritico.CierreInteracion = _context.TObsVCCCierreIteraccion
                                                                            .Where(t => t.CodVcc.Equals(obs_vcc.CodVcc))
                                                                            .ProjectTo<CierreInteraccionDto>(_mapper.ConfigurationProvider)
                                                                            .ToHashSet();
                    
                    var cartillaStr = verControlCritico.CodCartilla;

                    //var cartillaObj = await _general.GetCartilla(cartillaStr);
                    var cartillaObj = await _mediator.Send(new GetCartillaQuery() { CodCartilla = cartillaStr });

                    //if(cartillaObj.PeligroFatal.Equals("CCPF0000022")){
                    //    obsVM.Covid19 = verControlCritico;
                    //}
                    //else{
                    //    obsVM.VerificacionControlCritico = verControlCritico;
                    //}
                    if (obsVM.CodTipoObservacion == "4.2")
                    {
                        obsVM.VerificacionControlCritico = verControlCritico;
                    }
                    else
                    {
                        obsVM.Covid19 = verControlCritico;
                    }

                }

                //obsVM.PlanAccion = await _planAccion.Get(request.CodObservacion);
                //obsVM.PlanAccion = await _planAccion.Get("INSP0000008308");

                //obsVM.Files = 

                return obsVM;
            }
        }
    }
}
