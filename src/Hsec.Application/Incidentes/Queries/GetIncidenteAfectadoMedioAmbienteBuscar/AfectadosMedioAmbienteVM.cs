using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoBuscarMedioAmbiente
{
    public class AfectadosMedioAmbienteVM
    {
        public AfectadosMedioAmbienteVM()
        {
            list = new HashSet<DetalleAfectadoDto>();
        }
        public ICollection<DetalleAfectadoDto> list { get; set; }
    }
}
