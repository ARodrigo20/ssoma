using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command.VM
{
    public class PreguntasEvaluacionRespVM
    {
        public double nota { get; set; }
        public string estadoEvaluacion { get; set; }
        public int porcentajeEval { get; set; }
        public decimal? puntajeTotal { get; set; }
    }
}