using System.Collections.Generic;

namespace Hsec.Application.General.Personas.Queries.GetBuscarPersonas
{
    public class PersonasVM
    {
        //public IList<PriorityLevelDto> PriorityLevels { get; set; }

        public IList<PersonasDto> Lists { get; set; }

        public int Count { get; set; }
    }
}
