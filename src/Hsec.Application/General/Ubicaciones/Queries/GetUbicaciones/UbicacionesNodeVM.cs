using System.Collections.Generic;

namespace Hsec.Application.General.Ubicaciones.Queries.GetUbicaciones
{
    public class UbicacionesNodeVM
    {
        public UbicacionesNodeVM()
        {
            this.children = new HashSet<UbicacionesNodeVM>();
            //this.children = new List<UbicacionesNodeVM>();
        }
        public string label { get; set; }
        public string data { get; set; }
        public ICollection<UbicacionesNodeVM> children { get; set; }
        //public List<UbicacionesNodeVM> children { get; set; }
    }
}
