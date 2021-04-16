using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TAcreditacionCurso : AuditableEntity
    {
        public string? CodCurso { get; set; }
        public string? CodPersona { get; set; }
        public string? CodStiker { get; set; }
        public string? Candado { get; set; } // CodTarjeta       
        public DateTime? FechaStiker { get; set; }
        public DateTime? FechaTarjeta { get; set; }
    }
}