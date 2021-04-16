using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TAcceso : AuditableEntity
    {
        public int CodAcceso { get; set; }
        public int? CodPadre { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; } // texto alternativo al nombre- tooltip
        public string Componente { get; set; }
        public string Icono { get; set; }
        public string BadgeVariant { get; set; }
        public string BadgeText { get; set; }      
        public TAcceso Padre { get; set; }
        public ICollection<TAcceso> Hijos { get; set; }
        public ICollection<TRolAcceso> RolAccesos { get; set; }
        public TAcceso() {
            RolAccesos = new HashSet<TRolAcceso>();
            Hijos = new HashSet<TAcceso>();
        }

    }
}
