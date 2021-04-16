using System.Collections.Generic;

namespace Hsec.Application.General.TipoIncidente.Queries.GetTipoIncidente
{
    public class TipoIncidenteNodeVM
    {
        public TipoIncidenteNodeVM()
        {
            this.children = new HashSet<TipoIncidenteNodeVM>();
        }
        public string label { get; set; }
        public string data { get; set; }
        public ICollection<TipoIncidenteNodeVM> children { get; set; }
    }
}
