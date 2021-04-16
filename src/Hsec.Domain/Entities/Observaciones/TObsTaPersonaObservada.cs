using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObsTaPersonaObservada : AuditableEntity
    {
        public string CodPersonaMiembro { get; set; }
        public string CodObservacion { get; set; }
    }
}
