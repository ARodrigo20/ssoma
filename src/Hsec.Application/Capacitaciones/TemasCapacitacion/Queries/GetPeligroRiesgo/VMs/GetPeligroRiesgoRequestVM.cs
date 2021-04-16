using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetPeligroRiesgo.VMs
{
    public class GetPeligroRiesgoRequestVM
    {
        public int pagina { get; set; }
        public int tamanio { get; set; }
        public string codPeligro { get; set; }
        public string codRiesgo { get; set; }
        public string descRiesgo { get; set; }
        public string descPeligro { get; set; }

    }
}
