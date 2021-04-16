using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Participantes.Command.Update.DTOs
{
    public class UpdateParticipantesDto
    {
        public string codPersona { get; set; } // Primary Key 1
        public decimal? nota { get; set; }
        public bool tipo { get; set; } // Si es participante invitado o colado !!!
    }
}
