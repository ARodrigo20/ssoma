using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Queries
{
    public class PreguntaRespuestaVM
    {
        public string CodCurso { get; set; }
        public string CodPersona { get; set; }
        public int? CodPregunta { get; set; }
        public string Pregunta { get; set; }
        public string Tipo { get; set; }
        public string Respuesta { get; set; }
        public decimal? Nota { get; set; }
        public double Puntaje { get; set; }
    }
}
