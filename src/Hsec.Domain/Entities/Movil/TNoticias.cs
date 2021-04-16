using System;

namespace Hsec.Domain.Entities.Movil
{
    public class TNoticias
    {
        public string CodNoticia { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? FecCreacion { get; set; }
        public string UsuCreacion { get; set; }
        public DateTime? FecModifica { get; set; }
        public string UsuModifica { get; set; }
        public string Estado { get; set; }
    }
}
