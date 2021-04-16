using System.Collections.Generic;

namespace Hsec.Application.General.PlanAnualCartillas.Queries.GetPlanAnualSeguimiento
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