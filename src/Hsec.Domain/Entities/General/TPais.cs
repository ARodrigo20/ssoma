using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TPais : AuditableEntity
    {
        public TPais()
        {
            Personas = new HashSet<TPersona>();
        }
        public string CodPais { get; set; }
        public string Descripcion { get; set; }

        public ICollection<TPersona> Personas { get; set; }
    }
}
