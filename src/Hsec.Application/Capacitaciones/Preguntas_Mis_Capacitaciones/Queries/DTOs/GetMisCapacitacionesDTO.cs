using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Mis_Capacitaciones.Queries.DTOs
{
    public class GetMisCapacitacionesDTO
    {
        public string codTemaCapacita { get; set; }
        public string codCurso { get; set; }
        public string tema { get; set; }
        public DateTime fechaIni { get; set; }
        public DateTime fechaFinal { get; set; }
        public DateTime? fechaVen { get; set; }
        public int? vigencia { get; set; }
        public string codVigenciaCapacita { get; set; }
        public decimal? nota { get; set; }
        public int intentos { get; set; }
        public decimal? notabase { get; set; }
        public int porcentaje { get; set; }
        public string estado { get; set; }
        public bool certificado { get; set; }
    }
}
