using Hsec.Application.Capacitaciones.Preguntas.Queries.Get.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas.Queries.Get.VMs
{
    public class GetPreguntaVM
    {
        public GetPreguntaVM() {
            data = new List<GetPreguntaDTO>();
        }
        public IList<GetPreguntaDTO> data { get; set; }
        public int count { get; set; }
    }
}
