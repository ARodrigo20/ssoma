using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TAfectadoPropiedad: AuditableEntity
    {
        public string CodIncidente { get; set; }
        public int Correlativo { get; set; }
        public string CodTipActivo { get; set; }
        public string CodActivo { get; set; }
        public string Operador { get; set; }
        public string Daño { get; set; }
        public string CodCosto { get; set; }
        public decimal? Monto { get; set; }
        public string Descripcion { get; set; }
        public string CodTipoAfectado { get; set; }

        //public virtual TcostoDaño CodCostoNavigation { get; set; }
        public virtual TIncidente CodIncidenteNavigation { get; set; }
        //public virtual TtipoActivo CodTipActivoNavigation { get; set; }
    }
}
