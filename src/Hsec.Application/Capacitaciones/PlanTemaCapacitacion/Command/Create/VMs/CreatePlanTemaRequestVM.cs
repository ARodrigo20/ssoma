using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Create.VMs
{
    public class CreatePlanTemaRequestVM
    {
        public string codTemaCapacita { get; set; }
        public bool tipo { get; set; }
        public string codReferencia { get; set; } //key
    }
}
