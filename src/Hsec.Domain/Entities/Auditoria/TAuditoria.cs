using Hsec.Domain.Common;
using System;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.Auditoria
{
    public class TAuditoria : AuditableEntity
    {
        public TAuditoria()
        {
            TAuditoriaTregNoConfoObserva = new HashSet<TAuditoriaTregNoConfoObserva>();
            TEquipoAuditor = new HashSet<TEquipoAuditor>();
            THallazgos = new HashSet<THallazgos>();
        }

        public string CodAuditoria { get; set; }
        public string AuditoriaDescripcion { get; set; }
        public string CodTipoAuditoria { get; set; }
        public string CodAreaAlcance { get; set; }
        public string CodPosicionGer { get; set; }
        public string CodPosicionSup { get; set; }
        public string CodContrata { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string CodRespAuditoria { get; set; }

        public virtual ICollection<TAuditoriaTregNoConfoObserva> TAuditoriaTregNoConfoObserva { get; set; }
        public virtual ICollection<TEquipoAuditor> TEquipoAuditor { get; set; }
        public virtual ICollection<THallazgos> THallazgos { get; set; }
    }
}
