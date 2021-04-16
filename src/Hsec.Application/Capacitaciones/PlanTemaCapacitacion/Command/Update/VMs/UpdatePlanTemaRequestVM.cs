using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Update.VMs
{
    public class UpdatePlanTemaRequestVM
    {
        public string codTemaCapacita { get; set; }
        public bool tipo { get; set; }
        public string codReferencia { get; set; } //key
    }
}
