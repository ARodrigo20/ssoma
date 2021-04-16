using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TSecuenciaEvento: AuditableEntity
    {
        public string CodIncidente { get; set; }
        public int Correlativo { get; set; }
        public string CodTabla { get; set; }
        public string Orden { get; set; }
        public string Evento { get; set; }

        public virtual TIncidente CodIncidenteNavigation { get; set; }
    }
}
