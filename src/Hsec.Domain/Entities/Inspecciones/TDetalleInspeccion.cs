using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Inspecciones
{
    public class TDetalleInspeccion : AuditableEntity
    {
        public string CodInspeccion { get; set; }
        public long Correlativo { get; set; }
        public int NroDetInspeccion { get; set; }
        public string CodTabla { get; set; }
        public string Lugar { get; set; }
        public string CodUbicacion { get; set; }
        public string CodAspectoObs { get; set; }
        public string CodActividadRel { get; set; }
        public string Observacion { get; set; }
        public string CodNivelRiesgo { get; set; }
        public TInspeccion Inspeccion { get; set; }
    }
}
