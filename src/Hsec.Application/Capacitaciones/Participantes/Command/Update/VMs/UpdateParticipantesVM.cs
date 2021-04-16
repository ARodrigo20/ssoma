using Hsec.Application.Capacitaciones.Participantes.Command.Update.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Participantes.Command.Update.VMs
{
    public class UpdateParticipantesVM
    {
        public UpdateParticipantesVM() {
            participantes = new List<UpdateParticipantesDto>();
        }
        public string codTemaCapacita { get; set; } // Primary Key 2
        public IList<UpdateParticipantesDto> participantes { get; set; }       
       
    }
}
