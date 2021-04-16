using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.General
{
    public class THistorialAprobacion : AuditableEntity
    {
        public int Correlativo { get; set; }
        public int CodAprobacion { get; set; }
        public string CodAprobador { get; set; }
        public string Comentario { get; set; }
        public string EstadoAprobacion { get; set; }
        public TAprobacion Aprobacion { get; set; }

    }
}