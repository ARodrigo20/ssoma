using System.Collections.Generic;

namespace Hsec.Application.General.GestionRiesgo.Queries.GetGestionRiesgo
{
    public class GestionRiesgoVM
    {
        public GestionRiesgoVM()
        {
            this.data = new HashSet<GestionRiesgoNodeVM>();
        }
        public ICollection<GestionRiesgoNodeVM> data { get; set; }
        public int count { get; set; }
    }
}
