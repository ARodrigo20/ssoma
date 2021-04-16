using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class JerarquiaVM
    {
        public JerarquiaVM()
        {
            data = new List<JerarquiaNodeVM>();
        }
        public IList<JerarquiaNodeVM> data { get; set; }
        public int count { get; set; }        
    }
}
