using System.Collections.Generic;

namespace Hsec.Application.General.TipoIncidente.Queries.GetTipoIncidente
{
    public class TipoIncidenteVM
    {
        public TipoIncidenteVM()
        {
            this.data = new HashSet<TipoIncidenteNodeVM>();
        }
        public ICollection<TipoIncidenteNodeVM> data { get; set; }
        public int count { get; set; }
    }
}
