using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Files.Commands.CreateFiles;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.Observacion.Commands.CreateObservacion
{

    public class CreateObservacionCommand : IRequest<string>
    {
        public ObservacionDto data { get; set; }

        public class CreateObservacionCommandHandler : IRequestHandler<CreateObservacionCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            private readonly string RESPUESTA_INCORRECTA = "R002";

            public CreateObservacionCommandHandler(IApplicationDbContext context, IMapper mapper
                , IMediator mediator
                )
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<string> Handle(CreateObservacionCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    string CodObservacion = nextCod();
                    ObservacionDto obsNueva = request.data;
                    if (obsNueva.CodTipoObservacion.Length > 1)
                    {
                        obsNueva.CodSubTipoObs = obsNueva.CodTipoObservacion.Substring(2, 1);
                        obsNueva.CodTipoObservacion = obsNueva.CodTipoObservacion.Substring(0, 1);
                    }

                    TObservacion obs = _mapper.Map<ObservacionDto, TObservacion>(obsNueva);
                    obs.CodObservacion = CodObservacion;
                    _context.TObservaciones.Add(obs);
                    //await _context.SaveChangesAsync(cancellationToken);
                    //obs.CodObservacion = string.Format("OBS{0,10:D10}", obs.Correlativo);

                    if (obsNueva.Tarea != null)
                    {
                        TObservacionTarea tarea = _mapper.Map<TareaDto, TObservacionTarea>(obsNueva.Tarea);
                        tarea.CodObservacion = CodObservacion;
                        _context.TObservacionTareas.Add(tarea);
                        foreach (string persona in obsNueva.Tarea.PersonaObservadas)
                        {
                            TObsTaPersonaObservada temp = new TObsTaPersonaObservada();
                            temp.CodPersonaMiembro = persona;
                            temp.CodObservacion = CodObservacion;
                            _context.TObsTaPersonaObservadas.Add(temp);
                        }
                        foreach (RegistroEncuestaDto encuesta in obsNueva.Tarea.RegistroEncuestas)
                        {
                            TObsTaRegistroEncuesta temp = _mapper.Map<TObsTaRegistroEncuesta>(encuesta);
                            temp.CodObservacion = CodObservacion;
                            _context.TObsTaRegistroEncuestas.Add(temp);
                        }
                        int correlativo = 1;
                        foreach (EtapaTareaDto etapa in obsNueva.Tarea.EtapaTareas)
                        {
                            TObsTaEtapaTarea temp = _mapper.Map<TObsTaEtapaTarea>(etapa);
                            temp.CodObservacion = CodObservacion;
                            temp.Correlativo = correlativo++;
                            _context.TObsTaEtapaTareas.Add(temp);
                        }
                        int orden = 1;
                        foreach (ComentarioDto comentario in obsNueva.Tarea.Comentarios)
                        {
                            TObsTaComentario temp = _mapper.Map<TObsTaComentario>(comentario);
                            temp.CodObservacion = CodObservacion;
                            temp.Orden = orden++;
                            _context.TObsTaComentarios.Add(temp);
                        }
                    }
                    else if (obsNueva.Comportamiento != null)
                    {
                        TObservacionComportamiento comportamiento = _mapper.Map<ComportamientoDto, TObservacionComportamiento>(obsNueva.Comportamiento);
                        comportamiento.CodObservacion = obs.CodObservacion;
                        _context.TObservacionComportamientos.Add(comportamiento);
                    }
                    else if (obsNueva.Condicion != null)
                    {
                        TObservacionCondicion condicion = _mapper.Map<CondicionDto, TObservacionCondicion>(obsNueva.Condicion);
                        condicion.CodObservacion = obs.CodObservacion;
                        _context.TObservacionCondiciones.Add(condicion);
                    }
                    else if (obsNueva.IteraccionSeguridad != null)
                    {
                        TObservacionIteraccion iteraccion = _mapper.Map<IteraccionSeguridadDto, TObservacionIteraccion>(obsNueva.IteraccionSeguridad);
                        iteraccion.CodObservacion = obs.CodObservacion;

                        _context.TObservacionIteracciones.Add(iteraccion);

                        foreach (String CodDescripcion in obsNueva.IteraccionSeguridad.MetodologiaGestionRiesgos)
                        {
                            TObsISRegistroEncuesta temp = new TObsISRegistroEncuesta();
                            temp.CodObservacion = CodObservacion;
                            temp.CodDescripcion = CodDescripcion;
                            temp.CodEncuesta = TipoEncuestaIteraccion.MetodologiaGestionRiesgos.GetHashCode().ToString();
                            _context.TObsISRegistroEncuestas.Add(temp);
                        }
                        foreach (String CodDescripcion in obsNueva.IteraccionSeguridad.ActividadAltoRiesgo)
                        {
                            TObsISRegistroEncuesta temp = new TObsISRegistroEncuesta();
                            temp.CodObservacion = CodObservacion;
                            temp.CodDescripcion = CodDescripcion;
                            temp.CodEncuesta = TipoEncuestaIteraccion.ActividadAltoRiesgo.GetHashCode().ToString();
                            _context.TObsISRegistroEncuestas.Add(temp);
                        }
                        foreach (String CodDescripcion in obsNueva.IteraccionSeguridad.ClasificacionObservacion)
                        {
                            TObsISRegistroEncuesta temp = new TObsISRegistroEncuesta();
                            temp.CodObservacion = CodObservacion;
                            temp.CodDescripcion = CodDescripcion;
                            temp.CodEncuesta = TipoEncuestaIteraccion.ClasificacionObservacion.GetHashCode().ToString();
                            _context.TObsISRegistroEncuestas.Add(temp);
                        }
                        foreach (String CodDescripcion in obsNueva.IteraccionSeguridad.ComportamientoRiesgoCondicion)
                        {
                            TObsISRegistroEncuesta temp = new TObsISRegistroEncuesta();
                            temp.CodObservacion = CodObservacion;
                            temp.CodDescripcion = CodDescripcion;
                            temp.CodEncuesta = TipoEncuestaIteraccion.ComportamientoRiesgoCondicion.GetHashCode().ToString();
                            _context.TObsISRegistroEncuestas.Add(temp);
                        }
                    }
                    else if (obsNueva.VerificacionControlCritico != null)
                    {
                        TObservacionVerControlCritico vcc = _mapper.Map<VerificacionControlCriticoDto, TObservacionVerControlCritico>(obsNueva.VerificacionControlCritico);
                        vcc.CodObservacion = obs.CodObservacion;
                        string codigoVCC = nextCodVCC();
                        vcc.CodVcc = codigoVCC;
                        _context.TObservacionVerControlCritico.Add(vcc);
                        ICollection<string> herramientas = obsNueva.VerificacionControlCritico.Herramientas;
                        foreach (var item in herramientas)
                        {
                            var data = new TObsVCCHerramienta();
                            data.CodDesHe = item;
                            data.CodVcc = codigoVCC;
                            _context.TObsVCCHerramienta.Add(data);
                        }
                        ICollection<CriterioDto> Criterios = obsNueva.VerificacionControlCritico.Criterios;

                        var ControlCritico = Criterios.GroupBy(t => t.CodCC).Select(t => t.Key).ToList();
                        foreach (var item in ControlCritico)
                        {
                            var data = new TObsVCCVerCCEfectividad();
                            data.CodCC = item;
                            data.CodVcc = codigoVCC;
                            data.CodCartilla = obsNueva.VerificacionControlCritico.CodCartilla;
                            data.Efectividad = efectividad(Criterios).ToString();
                            _context.TObsVCCVerCCEfectividad.Add(data);
                        }

                        foreach (var item in Criterios)
                        {
                            var data = _mapper.Map<CriterioDto, TObsVCCRespuesta>(item);
                            data.CodVcc = codigoVCC;
                            _context.TObsVCCRespuesta.Add(data);

                        }
                        ICollection<CierreInteraccionDto> CierreInteraccion = obsNueva.VerificacionControlCritico.CierreInteracion;
                        foreach (var item in CierreInteraccion)
                        {
                            var data = _mapper.Map<CierreInteraccionDto, TObsVCCCierreIteraccion>(item);
                            data.CodVcc = codigoVCC;
                            _context.TObsVCCCierreIteraccion.Add(data);
                        }
                    }
                    else if (obsNueva.Covid19 != null)
                    {
                        TObservacionVerControlCritico vcc = _mapper.Map<VerificacionControlCriticoDto, TObservacionVerControlCritico>(obsNueva.Covid19);
                        vcc.CodObservacion = obs.CodObservacion;
                        string codigoVCC = nextCodVCC();
                        vcc.CodVcc = codigoVCC;
                        _context.TObservacionVerControlCritico.Add(vcc);
                        ICollection<string> herramientas = obsNueva.Covid19.Herramientas;
                        foreach (var item in herramientas)
                        {
                            var data = new TObsVCCHerramienta();
                            data.CodDesHe = item;
                            data.CodVcc = codigoVCC;
                            _context.TObsVCCHerramienta.Add(data);
                        }
                        ICollection<CriterioDto> Criterios = obsNueva.Covid19.Criterios;

                        var ControlCritico = Criterios.GroupBy(t => t.CodCC).Select(t => t.Key).ToList();
                        foreach (var item in ControlCritico)
                        {
                            var data = new TObsVCCVerCCEfectividad();
                            data.CodCC = item;
                            data.CodVcc = codigoVCC;
                            //data.CodCartilla = obsNueva.VerificacionControlCritico.CodCartilla;
                            data.CodCartilla = obsNueva.Covid19.CodCartilla;
                            data.Efectividad = efectividad(Criterios).ToString();
                            _context.TObsVCCVerCCEfectividad.Add(data);
                        }

                        foreach (var item in Criterios)
                        {
                            var data = _mapper.Map<CriterioDto, TObsVCCRespuesta>(item);
                            data.CodVcc = codigoVCC;
                            _context.TObsVCCRespuesta.Add(data);
                        }
                        ICollection<CierreInteraccionDto> CierreInteraccion = obsNueva.Covid19.CierreInteracion;
                        foreach (var item in CierreInteraccion)
                        {
                            var data = _mapper.Map<CierreInteraccionDto, TObsVCCCierreIteraccion>(item);
                            data.CodVcc = codigoVCC;
                            _context.TObsVCCCierreIteraccion.Add(data);
                        }
                    }

                    //try
                    //{
                    //    var r1 = await _context.SaveChangesAsync(cancellationToken);
                    //    var r2 = await _imagen.Upload(obsNueva.files, CodObservacion);
                    //    var r3 = await _planAccion.Create(obsNueva.PlanAccion, CodObservacion);
                    //}
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine(e);
                    //}

                    var r1 = await _context.SaveChangesAsync(cancellationToken);


                    //var r2 = await _imagen.Upload(obsNueva.files, CodObservacion);
                    var r2 = await _mediator.Send(new CreateListFilesCommand { File = obsNueva.files, NroDocReferencia = CodObservacion, NroSubDocReferencia = CodObservacion, CodTablaRef = "TOBS" });

                    //var r3 = await _planAccion.Create(obsNueva.PlanAccion, CodObservacion);
                    obsNueva.PlanAccion.ForEach(t => { t.docReferencia = CodObservacion; t.docSubReferencia = CodObservacion; });
                    var r3 = await _mediator.Send(new CreatePlanAccionCommand() { planes = obsNueva.PlanAccion });

                    return CodObservacion;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw e;
                }

            }

            private int efectividad(ICollection<CriterioDto> criterios)
            {
                foreach (var item in criterios)
                {
                    if (item.Respuesta.Equals(RESPUESTA_INCORRECTA)) return 0;
                }
                return 1;
            }
            public string nextCod()
            {
                var COD_INCIDENTE_MAX = _context.TObservaciones.Max(t => t.CodObservacion);
                if (COD_INCIDENTE_MAX == null) COD_INCIDENTE_MAX = "OBS00000001";
                else
                {
                    string numberStr = COD_INCIDENTE_MAX.Substring(3);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_INCIDENTE_MAX = String.Format("OBS{0,8:00000000}", max);
                }
                return COD_INCIDENTE_MAX;
            }

            public string nextCodVCC()
            {
                var COD_INCIDENTE_MAX = _context.TObservacionVerControlCritico.Max(t => t.CodVcc);
                if (COD_INCIDENTE_MAX == null) COD_INCIDENTE_MAX = "OBV00000001";
                else
                {
                    string numberStr = COD_INCIDENTE_MAX.Substring(3);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_INCIDENTE_MAX = String.Format("OBV{0,8:00000000}", max);
                }
                return COD_INCIDENTE_MAX;
            }
        }
    }
}
