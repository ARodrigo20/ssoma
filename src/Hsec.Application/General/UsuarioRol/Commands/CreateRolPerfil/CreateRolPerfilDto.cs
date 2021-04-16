using System.Collections.Generic;

namespace Hsec.Application.General.UsuarioRol.Commands.CreateRolPerfil
{
    public class CreateRolPerfilDto
    {
        public string codRol {get;set;}
        public string descripcion {get;set;}
        public IList<int> permisos {get;set;}
    }
}