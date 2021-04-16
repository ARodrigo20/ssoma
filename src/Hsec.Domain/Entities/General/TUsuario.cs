using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TUsuario : AuditableEntity
    {
        public int CodUsuario { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string CodPersona { get; set; }
        public bool TipoLogueo { get; set; }
        public string Token { get; set; }
       // public TPersona Persona { get; set; }
        public ICollection<TUsuarioRol> UsuarioRoles { get; set; }
        public TUsuario()
        {
            UsuarioRoles = new HashSet<TUsuarioRol>();
        }
    }
}
