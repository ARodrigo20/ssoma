using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.General
{
    public class TJerarquiaResponsable : AuditableEntity
    {
        public int CodPosicion { get; set; }
        public string CodPersona { get; set; }        
        public int CodTipo { get; set; }

    }
}
