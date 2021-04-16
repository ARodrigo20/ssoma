using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.Observaciones;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion {
    internal class ComportamientoSubHandler : Strategia {
        public ComportamientoSubHandler (IApplicationDbContext context, IMapper mapper) : base (context, mapper) { }

        public override void UpsertSubTipo (ObservacionDto data) {
            string CodObservacion = data.CodObservacion;
            var ComportamientoRequet = data.Comportamiento;
            TObservacionComportamiento comportamiento = _context.TObservacionComportamientos.Find (CodObservacion);
            if (comportamiento == null) {
                comportamiento = new TObservacionComportamiento ();
                _mapper.Map<ComportamientoDto, TObservacionComportamiento> (ComportamientoRequet, comportamiento);
                comportamiento.CodObservacion = CodObservacion;
                _context.TObservacionComportamientos.Add (comportamiento);
            } else {
                comportamiento = _mapper.Map<ComportamientoDto, TObservacionComportamiento> (ComportamientoRequet, comportamiento);
                comportamiento.Estado = true;
                _context.TObservacionComportamientos.Update (comportamiento);
            }
        }
        public override void DeleteSubtipo (string COD_OBSERVACION) {
            TObservacionComportamiento obs_com = _context.TObservacionComportamientos.Find (COD_OBSERVACION);
            if (obs_com != null) {
                obs_com.Estado = false;
                _context.TObservacionComportamientos.Update (obs_com);
            }
        }
    }

}