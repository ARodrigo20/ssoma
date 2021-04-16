using Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Cursos_Inscritos.Queries.DTOs
{
    public class GetPreguntasCursosInscritosDTO
    {
        public GetPreguntasCursosInscritosDTO() {
            expositores = new List<GetCursoDisponibleExpositorDTO>();
        }

        public string codCurso { get; set; }
        public string tema { get; set; }
        public DateTime fechIni { get; set; }
        public DateTime fechaFinal { get; set; }
        public DateTime fechaVen { get; set; }
        public int? vigencia { get; set; }
        public string vigenciatxt { get; set; }
        public string codVigenciaCapacita { get; set; }
        public string duracion { get; set; }
        public string horaFin { get; set; }
        public bool estadoCurso { get; set; }
        public string codTemaCapacita { get; set; }
       // public bool vencimiento { get; set; } 
        // data de cursos disponibles
        public string horaInicio { get; set; }
        public string tipo { get; set; }
        public decimal? puntajeTotal { get; set; }
        public int? capacidad { get; set; }
        public string codEmpresaCap { get; set; }
        public string EmpresaCap { get; set; }
        //public string expoCodPersona { get; set; }
        //public string expoName { get; set; }
        public string codParticipante { get; set; }
        public string enlace { get; set; }
        public bool online { get; set; }
        public int intentos { get; set; }
        public bool inscrito { get; set; }
        public int? disponibles { get; set; }
        public IList<GetCursoDisponibleExpositorDTO> expositores { get; set; }

    }
}