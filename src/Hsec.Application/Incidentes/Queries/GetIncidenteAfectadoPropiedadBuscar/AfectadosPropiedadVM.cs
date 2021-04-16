using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPropiedadBuscar
{
    public class AfectadosPropiedadVM
    {
        public AfectadosPropiedadVM()
        {
            list = new HashSet<DetalleAfectadoDto>();
        }
        public ICollection<DetalleAfectadoDto> list { get; set; }
    }
}
