using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Create.DTOs
{
    public class CreateTemaCapacitacionResponseDto
    {
        public int correlativo { get; set; }
        public string codTemaCapacita { get; set; }
        public string codPeligro { get; set; }
        public string codRiesgo { get; set; }
    }
}
