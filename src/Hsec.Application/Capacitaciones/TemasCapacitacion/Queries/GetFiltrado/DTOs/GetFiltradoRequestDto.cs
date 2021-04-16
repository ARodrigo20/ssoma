using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado.DTOs
{
    public class GetFiltradoRequestDto
    {
        public int correlativo { get; set; }
        public string codTemaCapacita { get; set; }
        public string codPeligro { get; set; }
        public string codRiesgo { get; set; }
        public bool estado { get; set; }
    }
}
