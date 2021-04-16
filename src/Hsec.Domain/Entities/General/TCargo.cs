using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.General
{
    public class TCargo : AuditableEntity
    {

        public string Ocupacion { get; set; }
        public string CodWebControl { get; set; }
        public string Descripcion { get; set; }
        
    }
}
