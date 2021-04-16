using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TEquipoInvestigacion: AuditableEntity
    {
        public string CodIncidente { get; set; }
        public int Correlativo { get; set; }
        public int? NroEquipo { get; set; }
        public string CodTabla { get; set; }
        public string CodPersona { get; set; }
        public string AreaDes { get; set; }
        public string Lider { get; set; }

        public virtual TIncidente CodIncidenteNavigation { get; set; }
    }
}
