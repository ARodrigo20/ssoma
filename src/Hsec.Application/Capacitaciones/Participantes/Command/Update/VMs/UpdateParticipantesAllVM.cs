using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Participantes.Command.Update.VMs
{
    public class UpdateParticipantesAllVM
    {
        public UpdateParticipantesAllVM() {
            data = new List<UpdateParticipantesVM>();
        }
        public IList<UpdateParticipantesVM> data { get; set; }
        public int count { get; set; }
    }
}
