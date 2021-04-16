using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObsTaComentario : AuditableEntity
    {
        public int Orden { get; set; }
        public string CodObservacion { get; set; }
        public string CodTipoComentario { get; set; }
        public string Descripcion { get; set; }

    }
}