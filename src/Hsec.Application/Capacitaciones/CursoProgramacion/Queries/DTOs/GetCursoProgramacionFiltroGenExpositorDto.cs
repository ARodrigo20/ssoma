using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs
{
    public class GetCursoProgramacionFiltroGenExpositorDto
    {
        public string codPersona { get; set; }
        public string codCurso { get; set; }
        public string nombres { get; set; }
        public bool tipo { get; set; } // 1 -> interno y 0 -> externo       
    }
}