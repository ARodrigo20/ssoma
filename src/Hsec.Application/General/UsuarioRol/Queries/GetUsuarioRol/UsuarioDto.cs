using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.UsuarioRol.Queries.GetUsuarioRol
{
    public class UsuarioDto
    {
        public string CodUsuario { get; set; }
        public string Usuario { get; set; }
        public string Ruc { get; set; }
        public string Rol { get; set; }
        public string TipoUsuario { get; set; }
        public bool TipoLogueo { get; set; }
        public string CodPersona { get; set; }

 
    }
}
