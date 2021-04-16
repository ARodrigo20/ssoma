using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TProveedor : AuditableEntity
    {
        public string CodProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string Ruc { get; set; }

        public virtual ICollection<TPersona> Personas { get; set; }
    }
}
