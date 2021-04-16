using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.General
{
    public class TRolAcceso : AuditableEntity
    {
        public int CodRol { get; set; }
        public int CodAcceso { get; set; }
        public TAcceso Acceso { get; set; }
        public TRol Rol { get; set; }
    }
}
