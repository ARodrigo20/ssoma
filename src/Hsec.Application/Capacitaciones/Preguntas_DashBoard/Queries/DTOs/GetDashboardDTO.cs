using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries.DTOs
{
    public class GetDashboardDTO
    {
        public string codCurso { get; set; }
        public string tema { get; set; }
        public DateTime fechaIni { get; set; }
        public DateTime fechaFinal { get; set; }
        public DateTime? fechaVen { get; set; }
        public int? vigencia { get; set; }
        public string codVigenciaCapacita { get; set; }
        public string duracion { get; set; }
        public bool vencimiento { get; set; }
        public string codTemaCapacita { get; set; }
    }
}