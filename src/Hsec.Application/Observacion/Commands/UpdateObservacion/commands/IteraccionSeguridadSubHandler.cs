using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion {
    internal class IteraccionSeguridadSubHandler : Strategia {
        public IteraccionSeguridadSubHandler (IApplicationDbContext context, IMapper mapper) : base (context, mapper) { }

        public override void UpsertSubTipo (ObservacionDto data) {
            string CodObservacion = data.CodObservacion;
            var IteraccionRequest = data.IteraccionSeguridad;
            TObservacionIteraccion iteraccionSeguridad = _context.TObservacionIteracciones.FirstOrDefault (o => o.Estado && o.CodObservacion.Equals (CodObservacion));
            if (iteraccionSeguridad == null) {
                iteraccionSeguridad = new TObservacionIteraccion ();
                _mapper.Map<IteraccionSeguridadDto, TObservacionIteraccion> (IteraccionRequest, iteraccionSeguridad);
                iteraccionSeguridad.CodObservacion = CodObservacion;
                //iteraccionSeguridad.Estado = true;
                _context.TObservacionIteracciones.Add (iteraccionSeguridad);
            } else {
                iteraccionSeguridad = _mapper.Map<IteraccionSeguridadDto, TObservacionIteraccion> (IteraccionRequest, iteraccionSeguridad);
                iteraccionSeguridad.Estado = true;
                _context.TObservacionIteracciones.Update (iteraccionSeguridad);
            }
        }
        public override void CreateDetalleSubTipo (ObservacionDto data) {
            string CodObservacion = data.CodObservacion;
            var IteraccionRequest = data.IteraccionSeguridad;
            var list = _context.TObsISRegistroEncuestas.Where (t => t.CodObservacion == CodObservacion).ToList ();
            _context.TObsISRegistroEncuestas.RemoveRange (list);

            foreach (String CodDescripcion in IteraccionRequest.MetodologiaGestionRiesgos) {
                TObsISRegistroEncuesta temp = new TObsISRegistroEncuesta ();
                temp.CodObservacion = CodObservacion;
                temp.CodDescripcion = (CodDescripcion);
                temp.CodEncuesta = TipoEncuestaIteraccion.MetodologiaGestionRiesgos.GetHashCode ().ToString ();
                _context.TObsISRegistroEncuestas.Add (temp);
            }
            foreach (String CodDescripcion in IteraccionRequest.ActividadAltoRiesgo) {
                TObsISRegistroEncuesta temp = new TObsISRegistroEncuesta ();
                temp.CodObservacion = CodObservacion;
                temp.CodDescripcion = (CodDescripcion);
                temp.CodEncuesta = TipoEncuestaIteraccion.ActividadAltoRiesgo.GetHashCode ().ToString ();
                _context.TObsISRegistroEncuestas.Add (temp);
            }
            foreach (String CodDescripcion in IteraccionRequest.ClasificacionObservacion) {
                TObsISRegistroEncuesta temp = new TObsISRegistroEncuesta ();
                temp.CodObservacion = CodObservacion;
                temp.CodDescripcion = (CodDescripcion);
                temp.CodEncuesta = TipoEncuestaIteraccion.ClasificacionObservacion.GetHashCode ().ToString ();
                _context.TObsISRegistroEncuestas.Add (temp);
            }
            foreach (String CodDescripcion in IteraccionRequest.ComportamientoRiesgoCondicion) {
                TObsISRegistroEncuesta temp = new TObsISRegistroEncuesta ();
                temp.CodObservacion = CodObservacion;
                temp.CodDescripcion = (CodDescripcion);
                temp.CodEncuesta = TipoEncuestaIteraccion.ComportamientoRiesgoCondicion.GetHashCode ().ToString ();
                _context.TObsISRegistroEncuestas.Add (temp);
            }
        }
        public override void DeleteSubtipo (string COD_OBSERVACION) {
            TObservacionIteraccion obs_iter = _context.TObservacionIteracciones.Find (COD_OBSERVACION);
            if (obs_iter != null) {
                obs_iter.Estado = false;
                _context.TObservacionIteracciones.Update (obs_iter);
            }
        }
        public override void DeleteDetalleSubtipo (string COD_OBSERVACION) {
            IList<TObsISRegistroEncuesta> tore = _context.TObsISRegistroEncuestas.Where (t => t.CodObservacion.Equals (COD_OBSERVACION)).ToList ();
            _context.TObsISRegistroEncuestas.RemoveRange (tore);
        }
    }
}