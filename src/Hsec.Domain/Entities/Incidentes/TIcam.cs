using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TIcam: AuditableEntity
    {
        public string CodIncidente { get; set; }
        public int Correlativo { get; set; }
        public string CodTipoIcamfactOrg { get; set; }
        public string FactOrg { get; set; }
        public string CodTipoCondEntIcam { get; set; }
        public string CondEnt { get; set; }
        public string CodTipoAccEquipIcam { get; set; }
        public string AccEquip { get; set; }
        public string CodTipoDefenAusen { get; set; }
        public string DefenAusen { get; set; }

        public virtual TIncidente CodIncidenteNavigation { get; set; }
    }
}
