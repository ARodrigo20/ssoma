using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPersonaBuscar
{
    public class AfectadosPersonaVM
    {
        public AfectadosPersonaVM()
        {
            list = new HashSet<DetalleAfectadoDto>();
        }
        public ICollection<DetalleAfectadoDto> list { get; set; }
    }
}
