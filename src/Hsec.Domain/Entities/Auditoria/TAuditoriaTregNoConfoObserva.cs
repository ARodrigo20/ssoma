using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Auditoria
{
  public class TAuditoriaTregNoConfoObserva : AuditableEntity
  {
    public string CodAuditoria { get; set; }
    public string CodNoConformidad { get; set; }

    public virtual TAuditoria CodAuditoriaNavigation { get; set; }
  }
}