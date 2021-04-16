using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command.DTOs
{
    public class PreguntaEvaluacionAlterDTO
    {
        public int codPregunta { get; set; }
        public string tipo { get; set; }
        public string respuesta { get; set; }
        public double nota { get; set; }
    }
}
