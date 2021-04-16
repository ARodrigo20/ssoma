using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObsTaRegistroEncuesta : AuditableEntity
    {
        public string CodObservacion { get; set; }
        public string CodPregunta { get; set; }
        public string CodRespuesta { get; set; }

    }
}