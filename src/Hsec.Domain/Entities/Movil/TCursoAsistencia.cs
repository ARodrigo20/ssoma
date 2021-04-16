using Hsec.Domain.Common;
using System;

namespace Hsec.Domain.Entities.Movil
{
    public class TCursoAsistencia
    {
        public string CodPersona { get; set; }
        public string CodCurso { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FecCreacion { get; set; }
        public string UsuCreacion { get; set; }
        public DateTime? FecModifica { get; set; }
        public string UsuModifica { get; set; }
        public string Estado { get; set; }
    }
}
