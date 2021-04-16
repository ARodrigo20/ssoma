using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Create.VMs
{
    public class CreateExpositorExternoVM
    {
        public int sexo { get; set; }
        public string nroDocumento { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string ocupacion { get; set; }
        public string empresa { get; set; }

    }
}
