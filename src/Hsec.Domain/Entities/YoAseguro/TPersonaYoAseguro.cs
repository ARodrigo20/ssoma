using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.YoAseguro
{
    public class TPersonaYoAseguro : AuditableEntity
    {
        public string CodYoAseguro { get; set; }
        public string CodPersona { get; set; }
        public TYoAseguro YoAseguro { get; set; }
    }
}
