using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TPeligro : AuditableEntity
    {
        public string CodPeligro { get; set; }
        public string CodTarea { get; set; }
        public string TipoPeligro { get; set; }
        public string DescripcionPeligro { get; set; }
        //public int? CodTipoAspecto { get; set; }
        //public int? CodGrupoAspecto { get; set; }
        //public virtual TgrupoAspecto CodGrupoAspectoNavigation { get; set; }
        //public virtual TtipoAspecto CodTipoAspectoNavigation { get; set; }
    }
}
