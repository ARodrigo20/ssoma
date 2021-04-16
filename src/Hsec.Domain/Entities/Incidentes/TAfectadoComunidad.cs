using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TAfectadoComunidad : AuditableEntity
    {
        public string CodIncidente { get; set; }
        public int Correlativo { get; set; }
        public string CodComuAfec { get; set; }
        public string CodMotivo { get; set; }
        public string Descripcion { get; set; }
        public string CodTipoAfectado { get; set; }


        //public virtual TComuAfectada CodComuAfecNavigation { get; set; }
        public virtual TIncidente CodIncidenteNavigation { get; set; }
        //public virtual TMotivo CodMotivoNavigation { get; set; }
    }
}
