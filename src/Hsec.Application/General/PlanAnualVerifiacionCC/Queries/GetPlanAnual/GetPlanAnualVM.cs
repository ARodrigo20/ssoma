using System.Collections.Generic;
using Hsec.Application.General.PlanAnualGeneral.Models;

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetPlanAnual
{
    public class GetPlanAnualGeneralVM
    {
        public int Pagina { get; set; }
        public int Count { get; set; }
        public ICollection<string> Codigos { get; set; }
        public ICollection<PersonaDto> list { get; set; }
        public GetPlanAnualGeneralVM()
        {
            this.list = new HashSet<PersonaDto>();
        }
    }
}