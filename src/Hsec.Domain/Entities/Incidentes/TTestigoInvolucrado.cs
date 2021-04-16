using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TTestigoInvolucrado: AuditableEntity
    {
        public string CodIncidente { get; set; }
        public int Correlativo { get; set; }
        public int? NroGrupo { get; set; }
        public string CodTabla { get; set; }
        public string CodPersona { get; set; }
        public string Manifestacion { get; set; }

        public virtual TIncidente CodIncidenteNavigation { get; set; }
    }
}
