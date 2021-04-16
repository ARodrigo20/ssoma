using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Auditoria
{
  public class TEquipoAuditor : AuditableEntity
  {
        public string CodAuditoria { get; set; }
        public long Correlativo { get; set; }
        public string CodTabla { get; set; }
        public int? NroEquipo { get; set; }
        public string CodPersona { get; set; }
        public string Lider { get; set; }

        public virtual TAuditoria CodAuditoriaNavigation { get; set; }
  }
}