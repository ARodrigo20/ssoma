using System.Collections.Generic;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class PlanAccionVM
    {
        public PlanAccionVM()
        {
            data = new List<PlanVM>();
        }
        //public IList<PlanAccionMaestroDto> data { get; set; }
        public IList<PlanVM> data { get; set; }
        public int count { get; set; }
    }
}
