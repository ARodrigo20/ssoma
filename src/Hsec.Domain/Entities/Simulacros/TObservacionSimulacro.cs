using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Simulacros
{
    public class TObservacionSimulacro : AuditableEntity
    {
        public string CodSimulacro { get; set; }
        public int Correlativo { get; set; }
        public string Hora { get; set; }
        public string Suceso { get; set; }
        public TSimulacro Simulacro { get; set; }
    }
}
