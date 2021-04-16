using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObsVCCHerramienta: AuditableEntity
    {
        public int CodHerram {get;set;}
        public string CodVcc {get;set;}
        public string CodDesHe {get;set;}
    }
}