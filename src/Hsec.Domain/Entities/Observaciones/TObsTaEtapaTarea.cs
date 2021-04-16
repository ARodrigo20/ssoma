using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObsTaEtapaTarea : AuditableEntity
    {
        public string CodObservacion { get; set; }
        public int Correlativo { get; set; }
        public string TituloEtapaTarea { get; set; }
        public string DescripcionEtapaTarea { get; set; }
        
    }
}