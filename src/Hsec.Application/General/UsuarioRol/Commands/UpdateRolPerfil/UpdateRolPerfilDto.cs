using System.Collections.Generic;

namespace Hsec.Application.General.UsuarioRol.Commands.UpdateRolPerfil
{
    public class UpdateRolPerfilDto
    {
        public string codRol {get;set;}
        public string descripcion {get;set;}
        public IList<int> permisos {get;set;}
    }
}