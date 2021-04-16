using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Commands.CreateLevantamientoTareas
{
    public class PlanCompVM
    {
        public PlanCompVM()
        {
            plan = new List<PlanRespVM>();
        }
        public IList<PlanRespVM> plan { get; set; }
    }
}
