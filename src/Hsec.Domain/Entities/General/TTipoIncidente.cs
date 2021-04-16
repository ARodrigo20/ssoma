using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TTipoIncidente : AuditableEntity
    {
        public TTipoIncidente()
        {
            this.Hijos = new HashSet<TTipoIncidente>();
        }
        public string CodTipoIncidente { get; set; }
        public string CodPadreTipoIncidente { get; set; }
        public string Descripcion { get; set; }
        public TTipoIncidente Padre { get; set; }
        public virtual ICollection<TTipoIncidente> Hijos { get; set; }
    }
}
