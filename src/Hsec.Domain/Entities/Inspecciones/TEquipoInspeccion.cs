using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Inspecciones
{
    public class TEquipoInspeccion : AuditableEntity
    {
        public string CodInspeccion { get; set; }
        public string CodPersona { get; set; }
        public long ? Correlativo { get; set; }
        public string Lider { get; set; }
        public string NroEquipo { get; set; }
        public TInspeccion Inspeccion { get; set; }
    }
}
