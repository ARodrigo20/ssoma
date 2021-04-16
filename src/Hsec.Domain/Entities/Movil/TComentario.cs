using System;

namespace Hsec.Domain.Entities.Movil
{
    public class TComentario
    {
        public int Correlativo { get; set; }
        public string NroReferencia { get; set; }
        public string CodPersona { get; set; }
        public string Comentario { get; set; }
        public DateTime? FecCreacion { get; set; }
        public string UsuCreacion { get; set; }
        public DateTime? FecModifica { get; set; }
        public string UsuModifica { get; set; }
        public string Estado { get; set; }
    }
}
