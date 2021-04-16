using Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command.VM
{
    public class PreguntaEvaluacionVM
    {
        public PreguntaEvaluacionVM()
        {
            data = new List<PreguntaEvaluacionAlterDTO>();
        }
        public IList<PreguntaEvaluacionAlterDTO> data { get; set; }
        public string codCurso { get; set; }
        public string codPersona { get; set; }
    }
}
