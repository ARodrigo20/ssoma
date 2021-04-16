using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TPreguntas : AuditableEntity
    {
        public TPreguntas()
        {
            Alternativas = new HashSet<TAlternativas>();
        }
        public string CodCurso { get; set; }
        public int CodPregunta { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public double Puntaje { get; set; }
        public string Respuesta { get; set; }
        public TCurso Curso { get; set; }
        //public bool Respondido { get; set; } si se respondio dicha pregunta se realiza la verificacion y se da el resultado
        //public string RespuestaMarcada { get; set; } es la respuesta o respuestas dada por el alumno
        public ICollection<TAlternativas> Alternativas { get; set; }
    }
}
