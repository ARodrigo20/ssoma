using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command.DTOs
{
    public class PreguntasEvaluacionDTO
    {
        public PreguntasEvaluacionDTO()
        {
            //alternativas = new List<PreguntasEvaluacionAlterDTO>();
        }
        public int codPregunta { get; set; }       
        public string tipo { get; set; }        
        public string respuesta { get; set; }
        //public IList<PreguntasEvaluacionAlterDTO> alternativas { get; set; }      
    }
}
