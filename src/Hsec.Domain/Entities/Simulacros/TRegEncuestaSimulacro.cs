using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Simulacros
{
    public class TRegEncuestaSimulacro : AuditableEntity
    {
        public string CodSimulacro { get; set; }
        public string CodTabla { get; set; }
        public string CodPregunta { get; set; }
        public string CodRespuesta { get; set; }
        public TSimulacro Simulacro { get; set; }
    }
}