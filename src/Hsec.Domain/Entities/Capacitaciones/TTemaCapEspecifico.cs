using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TTemaCapEspecifico : AuditableEntity
    {       
        public string CodTemaCapacita { get; set; }
        public string? CodPeligro { get; set; }
        public string? CodRiesgo { get; set; }
        public TTemaCapacitacion TemaCapacitacion { get; set; }
    }
}
