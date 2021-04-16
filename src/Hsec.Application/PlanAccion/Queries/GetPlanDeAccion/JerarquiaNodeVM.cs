using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class JerarquiaNodeVM
    {
        public JerarquiaNodeVM()
        {
            children = new List<JerarquiaNodeVM>();
        }
        public string label { get; set; }
        public string data { get; set; }
        //public IList<JerarquiaNodeVM> children { get; set; }
        public int count { get; set; }
        public string tipo { get; set; }
        public IList<JerarquiaNodeVM> children { get; set; }
        public bool check { get; set; }
        //public string codPersona { get; set; }
        //public string nombres { get; set; }
        //public string apellidoPaterno { get; set; }
        //public string apellidoMaterno { get; set; }    
    }
}
