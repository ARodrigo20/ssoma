using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Auditoria
{
  public class THallazgos: AuditableEntity
  {
    public string CodHallazgo { get; set; }
    public string CodAuditoria { get; set; }
    public string CodTabla { get; set; }
    public string CodTipoHallazgo { get; set; }
    public virtual TAuditoria CodNavigateAuditoria {get;set;}
  }
}