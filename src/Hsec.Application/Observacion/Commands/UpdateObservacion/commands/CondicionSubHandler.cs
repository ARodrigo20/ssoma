using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.Observaciones;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion {
    internal class CondicionSubHandler : Strategia {
        public CondicionSubHandler (IApplicationDbContext context, IMapper mapper) : base (context, mapper) { }

        public override void UpsertSubTipo (ObservacionDto data) {
            string CodObservacion = data.CodObservacion;
            var CondicionRequest = data.Condicion;
            TObservacionCondicion condicion = _context.TObservacionCondiciones.Find (CodObservacion);
            if (condicion == null) {
                condicion = new TObservacionCondicion ();
                _mapper.Map<CondicionDto, TObservacionCondicion> (CondicionRequest, condicion);
                condicion.CodObservacion = CodObservacion;
                _context.TObservacionCondiciones.Add (condicion);
            } else {
                condicion = _mapper.Map<CondicionDto, TObservacionCondicion> (CondicionRequest, condicion);
                condicion.Estado = true;
                _context.TObservacionCondiciones.Update (condicion);
            }
        }
        public override void DeleteSubtipo (string COD_OBSERVACION) {
            TObservacionCondicion obs_con = _context.TObservacionCondiciones.Find (COD_OBSERVACION);
            if (obs_con != null) {
                obs_con.Estado = false;
                _context.TObservacionCondiciones.Update (obs_con);
            }
        }
    }
}