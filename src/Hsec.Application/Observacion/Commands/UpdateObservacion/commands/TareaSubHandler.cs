using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.Observaciones;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion {
    internal class TareaSubHandler : Strategia {
        public TareaSubHandler (IApplicationDbContext context, IMapper mapper) : base (context, mapper) { }

        public override void UpsertSubTipo (ObservacionDto data) {
            string CodObservacion = data.CodObservacion;
            var TareaRequest = data.Tarea;

            TObservacionTarea tarea = _context.TObservacionTareas.Find (CodObservacion);
            if (tarea == null) {
                tarea = new TObservacionTarea ();
                _mapper.Map<TareaDto, TObservacionTarea> (TareaRequest, tarea);
                tarea.CodObservacion = CodObservacion;
                _context.TObservacionTareas.Add (tarea);
            } else {
                tarea = _mapper.Map<TareaDto, TObservacionTarea> (TareaRequest, tarea);
                _context.TObservacionTareas.Update (tarea);
            }
        }
        public override void CreateDetalleSubTipo (ObservacionDto data) {
            string CodObservacion = data.CodObservacion;
            var TareaRequest = data.Tarea;
            foreach (string persona in TareaRequest.PersonaObservadas) {
                TObsTaPersonaObservada temp = new TObsTaPersonaObservada ();
                temp.CodObservacion = CodObservacion;
                temp.CodPersonaMiembro = persona;
                _context.TObsTaPersonaObservadas.Add (temp);
            }
            foreach (RegistroEncuestaDto encuesta in TareaRequest.RegistroEncuestas) {
                TObsTaRegistroEncuesta temp = _mapper.Map<TObsTaRegistroEncuesta> (encuesta);
                temp.CodObservacion = CodObservacion;
                _context.TObsTaRegistroEncuestas.Add (temp);
            }
            int correlativo = 1;
            foreach (EtapaTareaDto etapa in TareaRequest.EtapaTareas) {
                TObsTaEtapaTarea temp = _mapper.Map<TObsTaEtapaTarea> (etapa);
                temp.CodObservacion = CodObservacion;
                temp.Correlativo = correlativo++;
                _context.TObsTaEtapaTareas.Add (temp);
            }
            int orden = 1;
            foreach (ComentarioDto comentario in TareaRequest.Comentarios) {
                TObsTaComentario temp = _mapper.Map<TObsTaComentario> (comentario);
                temp.CodObservacion = CodObservacion;
                temp.Orden = orden++;
                _context.TObsTaComentarios.Add (temp);
            }
        }
        public override void DeleteSubtipo (string COD_OBSERVACION) {
            TObservacionTarea obs_tar = _context.TObservacionTareas.Find (COD_OBSERVACION);
            if (obs_tar != null) {
                obs_tar.Estado = false;
                _context.TObservacionTareas.Update (obs_tar);
            }
        }
        public override void DeleteDetalleSubtipo (string COD_OBSERVACION) {
            IList<TObsTaComentario> totc = _context.TObsTaComentarios.Where (t => t.CodObservacion.Equals (COD_OBSERVACION)).ToList ();
            _context.TObsTaComentarios.RemoveRange (totc);
            IList<TObsTaEtapaTarea> totet = _context.TObsTaEtapaTareas.Where (t => t.CodObservacion.Equals (COD_OBSERVACION)).ToList ();
            _context.TObsTaEtapaTareas.RemoveRange (totet);
            IList<TObsTaPersonaObservada> totpo = _context.TObsTaPersonaObservadas.Where (t => t.CodObservacion.Equals (COD_OBSERVACION)).ToList ();
            _context.TObsTaPersonaObservadas.RemoveRange (totpo);
            IList<TObsTaRegistroEncuesta> totre = _context.TObsTaRegistroEncuestas.Where (t => t.CodObservacion.Equals (COD_OBSERVACION)).ToList ();
            _context.TObsTaRegistroEncuestas.RemoveRange (totre);
        }
    }

}