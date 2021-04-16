using Hsec.Domain.Common;
using System;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.Auditoria
{
    public class TAudCartilla : AuditableEntity
    {
        public TAudCartilla()
        {
            TAudCCCriterio = new HashSet<TAudCCCriterio>();
        }

        public string CodAuditoria { get; set; }
        public string CodCartilla { get; set; }
        public string CodPeligroFatal { get; set; }
        public string Descripcion { get; set; }

        public ICollection<TAudCCCriterio> TAudCCCriterio { get; set; }
    }
}
