using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Otros
{
    public class TAusentesReunion : AuditableEntity
    {
        public string CodReunion { get; set; }

        public string CodPersona { get; set; }

        public TReunion Reunion { get; set; }
    }
}
