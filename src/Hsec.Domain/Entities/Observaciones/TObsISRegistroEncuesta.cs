using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObsISRegistroEncuesta : AuditableEntity
    {
        public string CodObservacion { get; set; }
        public string CodDescripcion { get; set; }  //Maestro
        public string CodEncuesta { get; set; }  //Maestro

    }
}