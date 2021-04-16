using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.General
{
    public class TUsuarioRol : AuditableEntity
    {
        public int CodUsuario { get; set; }
        public int CodRol { get; set; }
        public TUsuario Usuario { get; set; }
        public TRol Rol { get; set; }
    }
}
