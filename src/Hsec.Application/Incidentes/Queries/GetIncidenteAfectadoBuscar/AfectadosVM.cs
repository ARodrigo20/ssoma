using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoBuscar
{
    public class AfectadosVM
    {
        public AfectadosVM()
        {
            list = new HashSet<DetalleAfectadoDto>();
        }
        public ICollection<DetalleAfectadoDto> list { get; set; }
    }
}
