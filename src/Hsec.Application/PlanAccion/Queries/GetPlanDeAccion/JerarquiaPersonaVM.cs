using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class JerarquiaPersonaVM
    {
        public JerarquiaPersonaVM()
        {
            Data = new List<JerarquiasPersonaNodeVM>();
        }
        public IList<JerarquiasPersonaNodeVM> Data { get; set; }
        public int count { get; set; }
    }
}
