using System.Collections.Generic;

namespace Hsec.Application.Incidentes.Queries.GetBuscarInsidentes
{
    public class IncidentesVM
    {
        public HashSet<IncidenteBuscarDto> Lists { get;set;}
        public int Count { get; set; }
        public IncidentesVM()
        {
        }
    }
}