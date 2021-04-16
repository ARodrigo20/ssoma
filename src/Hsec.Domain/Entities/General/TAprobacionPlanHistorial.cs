using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.General
{
    public class TAprobacionPlanHistorial : AuditableEntity
    {
        public TAprobacionPlanHistorial()
        {
        }

        public int Correlativo { get; set; }
        public int CodAprobacion { get; set; }
        public string CodPersona { get; set; }
        public string Comentario { get; set; }
        public string EstadoAprobacion { get; set; }
    }
}