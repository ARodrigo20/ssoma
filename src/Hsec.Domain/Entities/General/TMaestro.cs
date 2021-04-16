using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.General
{
    public class TMaestro : AuditableEntity
    {
        public string CodTabla { get; set; }
        public string CodTipo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }
        public string CodTablaRef { get; set; }
    }
}
