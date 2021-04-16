using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TRol : AuditableEntity
    {
        public int CodRol { get; set; }
        public string Descripcion { get; set; }

        public ICollection<TRolAcceso> RolAccesos { get; set; }
        public ICollection<TUsuarioRol> UsuarioRoles { get; set; }

        public TRol()
        {
            RolAccesos = new HashSet<TRolAcceso>();
            UsuarioRoles= new HashSet<TUsuarioRol>();
        }
    }
}
