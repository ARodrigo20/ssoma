using Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.VMs
{
    public class GetCursosDisponiblesVM
    {
        public GetCursosDisponiblesVM()
        {
            data = new List<GetCursosDisponiblesDTO>();
        }

        public IList<GetCursosDisponiblesDTO> data { get; set; }
        public int count { get; set; }
    }
}
