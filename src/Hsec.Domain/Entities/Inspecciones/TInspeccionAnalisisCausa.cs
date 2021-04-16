using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Inspecciones
{
    public class TInspeccionAnalisisCausa : AuditableEntity
    {
        public long Correlativo { get; set; }
        public string CodInspeccion { get; set; }
        public string CodAnalisis { get; set; }
        public string CodCausa { get; set; }
        public string CodCondicion { get; set; }
        public string Descripcion { get; set; }
        public TInspeccion Inspeccion { get; set; }
    }
}
