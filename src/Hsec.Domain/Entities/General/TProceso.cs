using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.General
{
    public class TProceso: AuditableEntity
    {
        public string CodProceso { get; set; }
        public string CadenaAprobacion { get; set; }
        public string Descripcion { get; set; }
        public string CodTabla { get; set; }
    }
}
