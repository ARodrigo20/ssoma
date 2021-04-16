using System.Collections.Generic;
using Hsec.Application.General.PlanAnualGeneral.Models;

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetPlanAnualSeguimiento
{
    public class GetPlanAnualSeguimientoVM
    {
        public int Pagina { get; set; }
        public int Count { get; set; }
        public ICollection<string> Codigos { get; set; }
        public ICollection<PersonaPSADto> ListPersona { get; set; }
        public GetPlanAnualSeguimientoVM()
        {
            this.ListPersona = new HashSet<PersonaPSADto>();
        }
    }
}