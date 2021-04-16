using System.Collections.Generic;

namespace Hsec.Application.General.GestionRiesgo.Queries.GetGestionRiesgo
{
    public class GestionRiesgoNodeVM
    {
        public GestionRiesgoNodeVM()
        {
            this.children = new HashSet<GestionRiesgoNodeVM>();
        }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string DetalleAsociado { get; set; }
        public ICollection<GestionRiesgoNodeVM> children { get; set; }
    }
}
