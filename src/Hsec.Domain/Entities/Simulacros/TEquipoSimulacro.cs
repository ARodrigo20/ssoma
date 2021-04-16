using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Simulacros
{
    public class TEquipoSimulacro : AuditableEntity
    {
        public string CodSimulacro { get; set; }
        public int Correlativo { get; set; }
        public string CodTabla { get; set; }
        public string CodPersona { get; set; }
        public int NroEquipo { get; set; }
        public string Lider { get; set; }
        public TSimulacro Simulacro { get; set; }
    }
}
