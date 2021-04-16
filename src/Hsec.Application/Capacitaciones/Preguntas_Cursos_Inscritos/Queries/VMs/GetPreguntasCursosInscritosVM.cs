using Hsec.Application.Capacitaciones.Preguntas_Cursos_Inscritos.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Cursos_Inscritos.Queries.VMs
{
    public class GetPreguntasCursosInscritosVM
    {
        public GetPreguntasCursosInscritosVM()
        {
            data = new List<GetPreguntasCursosInscritosDTO>();
        }

        public IList<GetPreguntasCursosInscritosDTO> data { get; set; }
        public int count { get; set; }





    }
}
