using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Expositores.Command.Update.VMs
{
    public class UpdateExpositorVM
    {
        public string codPersona { get; set; }
        public string codTemaCapacita { get; set; }
        public bool tipo { get; set; } // 1 -> interno y 0 -> externo
    }
}
