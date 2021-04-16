using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TParticipantes : AuditableEntity
    {
        public string CodPersona { get; set; } // Primary Key 1
        public string CodCurso { get; set; } // Primary Key 2
        public decimal? Nota { get; set; }
        public bool Evaluado { get; set; } //si se le asigana nota final 1
        public int Intentos { get; set; } //si se le asigana nota final 1
        public bool Tipo { get; set; } // Si es participante invitado o colado !!!
        public TCurso Curso { get; set; }
    }
}
