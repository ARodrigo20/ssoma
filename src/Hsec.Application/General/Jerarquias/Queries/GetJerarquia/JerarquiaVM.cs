using System.Collections.Generic;

namespace Hsec.Application.General.Jerarquias.Queries.GetJerarquia
{
    public class JerarquiaVM
    {
        public JerarquiaVM() {
            data = new List<JerarquiaNodeVM>();
        }
        public IList<JerarquiaNodeVM> data { get; set; }
        public int count { get; set; }
       
    }
}
