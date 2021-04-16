using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.PlanAccion.Commands.DeleteDocRefHsec;
using Hsec.Application.Files.Commands.DeleteFileDocRef;

namespace Hsec.Application.Observacion.Commands.DeleteObservacion
{
    public class DeleteObservacionCommand : IRequest<Unit>
    {

        public string CodObservacion { get; set; }

        public class DeleteObservacionCommandHandler : IRequestHandler<DeleteObservacionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public DeleteObservacionCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(DeleteObservacionCommand request, CancellationToken cancellationToken)
            {
                string COD_OBSERVACION = (request.CodObservacion);

                TObservacion obs = _context.TObservaciones.Find(COD_OBSERVACION);
                obs.Estado = false;
                _context.TObservaciones.Update(obs);

                if(obs.CodTipoObservacion == 1) //TipoObservacion.Comportamiento
                {
                    eliminarComportamiento(COD_OBSERVACION);
                }
                else if(obs.CodTipoObservacion == 2) //TipoObservacion.Condicion
                {
                    eliminarCondicion(COD_OBSERVACION);
                }
                else if(obs.CodTipoObservacion == 3) //TipoObservacion.Tarea
                {
                    eliminarTarea(COD_OBSERVACION);
                }
                else if(obs.CodTipoObservacion == 4 && obs.CodSubTipoObs == "1") //TipoObservacion.Iteraccion_Seguridad
                {
                    eliminarIteraccionSeguridad(COD_OBSERVACION);
                }
                else if(obs.CodTipoObservacion == 4 && obs.CodSubTipoObs == "1") //TipoObservacion.Iteraccion_Seguridad
                {
                    eliminarIteraccionSeguridad(COD_OBSERVACION);
                }
                else if(obs.CodTipoObservacion == 4 && obs.CodSubTipoObs == "1") // TipoObservacion.Iteraccion_Seguridad
                {
                    eliminarVerificacionControlCritico(COD_OBSERVACION);
                }
                    
                    
                var r1 = await _context.SaveChangesAsync(cancellationToken);

                //var r2 = await _planAccion.DeleteByCodRef(COD_OBSERVACION);
                var r2 = await _mediator.Send(new DeleteDocRefCommand() { NroDocReferencia = COD_OBSERVACION });

                //var r3 = await _imagen.DeleteByRefe(COD_OBSERVACION);
                var r3 = await _mediator.Send(new DeleteFileDocRefCommand() { NroDocReferencia = COD_OBSERVACION });

                return Unit.Value;
            }

            private void eliminarVerificacionControlCritico(string COD_OBSERVACION)
            {
                TObservacionVerControlCritico obs_vcc = _context.TObservacionVerControlCritico.Find(COD_OBSERVACION);
                obs_vcc.Estado = false;
                _context.TObservacionVerControlCritico.Remove(obs_vcc);

                var list_Herramienta = _context.TObsVCCHerramienta.Where(t => t.CodVcc.Equals(COD_OBSERVACION));
                _context.TObsVCCHerramienta.RemoveRange(list_Herramienta);
                var list_VerCCEfectividad = _context.TObsVCCVerCCEfectividad.Where(t => t.CodVcc.Equals(COD_OBSERVACION));
                _context.TObsVCCVerCCEfectividad.RemoveRange(list_VerCCEfectividad);
                var list_Respuesta = _context.TObsVCCRespuesta.Where(t => t.CodVcc.Equals(COD_OBSERVACION));
                _context.TObsVCCRespuesta.RemoveRange(list_Respuesta);
                var list_CierreIteraccion = _context.TObsVCCCierreIteraccion.Where(t => t.CodVcc.Equals(COD_OBSERVACION));
                _context.TObsVCCCierreIteraccion.RemoveRange(list_CierreIteraccion);
            }

            private void eliminarComportamiento(string COD_OBSERVACION)
            {
                TObservacionComportamiento obs_com = _context.TObservacionComportamientos.Find(COD_OBSERVACION);
                obs_com.Estado = false;
                _context.TObservacionComportamientos.Remove(obs_com);
            }
            private void eliminarCondicion(string COD_OBSERVACION)
            {
                TObservacionCondicion obs_con = _context.TObservacionCondiciones.Find(COD_OBSERVACION);
                obs_con.Estado = false;
                _context.TObservacionCondiciones.Remove(obs_con);
            }
            private void eliminarTarea(string COD_OBSERVACION)
            {
                TObservacionTarea obs_tar = _context.TObservacionTareas.Find(COD_OBSERVACION);
                obs_tar.Estado = false;
                _context.TObservacionTareas.Remove(obs_tar);

                    IList<TObsTaComentario> totc = _context.TObsTaComentarios.Where(t => t.CodObservacion.Equals(COD_OBSERVACION)).ToList();
                    _context.TObsTaComentarios.RemoveRange(totc);
                    IList<TObsTaEtapaTarea> totet = _context.TObsTaEtapaTareas.Where(t => t.CodObservacion.Equals(COD_OBSERVACION)).ToList();
                    _context.TObsTaEtapaTareas.RemoveRange(totet);
                    IList<TObsTaPersonaObservada> totpo = _context.TObsTaPersonaObservadas.Where(t => t.CodObservacion.Equals(COD_OBSERVACION)).ToList();
                    _context.TObsTaPersonaObservadas.RemoveRange(totpo);
                    IList<TObsTaRegistroEncuesta> totre = _context.TObsTaRegistroEncuestas.Where(t => t.CodObservacion.Equals(COD_OBSERVACION)).ToList();
                    _context.TObsTaRegistroEncuestas.RemoveRange(totre);

            }
            private void eliminarIteraccionSeguridad(string COD_OBSERVACION)
            {
                TObservacionIteraccion obs_iter = _context.TObservacionIteracciones.Find(COD_OBSERVACION);
                obs_iter.Estado = false;
                _context.TObservacionIteracciones.Remove(obs_iter);

                    IList<TObsISRegistroEncuesta> tore = _context.TObsISRegistroEncuestas.Where(t => t.CodObservacion.Equals(COD_OBSERVACION)).ToList();
                    _context.TObsISRegistroEncuestas.RemoveRange(tore);

            }

        }
    }
}
