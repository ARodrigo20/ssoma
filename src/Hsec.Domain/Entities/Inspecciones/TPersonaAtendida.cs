using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Inspecciones
{
    public class TPersonaAtendida : AuditableEntity
    {
        public string CodInspeccion { get; set; }
        public string CodPersona { get; set; }
        public long Correlativo { get; set; }
        public string CodTabla { get; set; }
        public TInspeccion Inspeccion { get; set; }
    }
}
