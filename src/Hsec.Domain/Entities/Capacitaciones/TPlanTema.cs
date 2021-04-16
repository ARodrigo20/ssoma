using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TPlanTema : AuditableEntity
    {
        public string? CodTemaCapacita { get; set; } //key
        public bool Tipo { get; set; } 
        public string CodReferencia { get; set; } //ACA GUARDAMOS LA POSICION
        public TTemaCapacitacion TemaCapacitacion { get; set; }
    }
}