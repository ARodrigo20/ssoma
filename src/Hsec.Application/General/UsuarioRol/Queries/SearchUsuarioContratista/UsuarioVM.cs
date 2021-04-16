using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.UsuarioRol.Queries.SearchUsuario
{
    public class UsuarioVM
    {
        public HashSet<SearchUsuarioContratistaDto> List { get; set; }
        public int Count { get; set; }
        public int Pagina { get; set; }
    }
}