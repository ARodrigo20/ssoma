using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.DTOs
{
    public class GetCursoDisponibleExpositorDTO
    {
        public string codPersona { get; set; }
        public string codTemaCapacita { get; set; }
        public bool tipo { get; set; }
        public string nombre { get; set; }
    }
}
