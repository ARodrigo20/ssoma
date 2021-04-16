using Hsec.Application.Capacitaciones.Preguntas.Command.Update.DTOs;
using Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command.VM
{
    public class PreguntasEvaluacionVM
    {
        public PreguntasEvaluacionVM()
        {
            data = new List<PreguntasEvaluacionDTO>();
        }
        public IList<PreguntasEvaluacionDTO> data { get; set; }        
        public string codCurso { get; set; }
        public string codPersona { get; set; }
    }
}
