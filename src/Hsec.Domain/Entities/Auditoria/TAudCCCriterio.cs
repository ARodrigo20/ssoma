using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Auditoria
{
  public class TAudCCCriterio: AuditableEntity
  {
    public string CodAuditoria { get; set; }
    public string CodCartilla { get; set; }
    public string CodCC { get; set; }
    public string CodCriterio { get; set; }
    public float Resultado { get; set; }
    // public string Documento { get; set; }

    public TAudCartilla CodNavigateTAudCartilla {get;set;}
  }
}