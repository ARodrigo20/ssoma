using System.Collections.Generic;

namespace Hsec.Application.General.Ubicaciones.Queries.GetUbicaciones
{
    public class UbicacionesVM
    {
        public UbicacionesVM()
        {
            this.data = new HashSet<UbicacionesNodeVM>();
        }
        public ICollection<UbicacionesNodeVM> data { get; set; }
        public int count { get; set; }
    }
}
