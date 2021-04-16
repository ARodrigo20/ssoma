using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TDetalleAfectado : AuditableEntity
    {
        public string CodIncidente { get; set; }
        public int Correlativo { get; set; }
        public string DesSuceso { get; set; }
        public string DesDanioLesImpacPerd { get; set; }
        public string AccInmediatas { get; set; }

        public virtual TIncidente CodIncidenteNavigation { get; set; }
    }
}
