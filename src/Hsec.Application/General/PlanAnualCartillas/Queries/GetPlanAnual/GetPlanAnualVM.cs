using System.Collections.Generic;
using Hsec.Application.General.PlanAnual.Models;

namespace Hsec.Application.General.PlanAnual.Queries.GetPlanAnual
{
    public class GetPlanAnualVM
    {
        public int Pagina { get; set; }
        public int Count { get; set; }
        public ICollection<string> Codigos { get; set; }
        public ICollection<PersonaDto> list { get; set; }
        public GetPlanAnualVM()
        {
            this.list = new HashSet<PersonaDto>();
        }
    }
}