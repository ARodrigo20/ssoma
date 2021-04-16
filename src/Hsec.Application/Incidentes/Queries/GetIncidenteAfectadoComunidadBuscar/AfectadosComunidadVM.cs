using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoComunidadBuscar
{
    public class AfectadosComunidadVM
    {
        public AfectadosComunidadVM()
        {
            list = new HashSet<DetalleAfectadoDto>();
        }
        public ICollection<DetalleAfectadoDto> list { get; set; }
    }
}
