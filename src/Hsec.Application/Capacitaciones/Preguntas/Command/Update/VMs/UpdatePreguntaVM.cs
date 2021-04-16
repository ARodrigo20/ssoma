using Hsec.Application.Capacitaciones.Preguntas.Command.Update.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas.Command.Update.VMs
{
    public class UpdatePreguntaVM
    {
        public UpdatePreguntaVM() {
            data = new List<UpdatePreguntaDTO>();
        }
        public IList<UpdatePreguntaDTO> data { get; set; }
        public string codCurso { get; set; }
    }
}