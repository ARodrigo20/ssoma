using Hsec.Application.Capacitaciones.Preguntas.Command.Create.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas.Command.Create.VMs
{
    public class CreatePreguntaVM
    {
        public CreatePreguntaVM()
        {
            alternativas = new List<AlternativasDTO>();
        }
        public string codCurso { get; set; }
        public int codPregunta { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public double puntaje { get; set; }
        public string respuesta { get; set; }
        public IList<AlternativasDTO> alternativas { get; set; }
    }
}
