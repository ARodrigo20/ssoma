using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Update.Dtos
{
    public class UpdateTemaCapacitacionRequestDto
    {        
        public string codTemaCapacita { get; set; }
        public string codPeligro { get; set; }
        public string codRiesgo { get; set; }
    }
}
