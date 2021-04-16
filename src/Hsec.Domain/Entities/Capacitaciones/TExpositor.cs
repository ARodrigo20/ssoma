using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TExpositor : AuditableEntity
    {
        public string CodPersona { get; set; }
        public string CodCurso { get; set; }
        public bool Tipo { get; set; } // 1 -> interno y 0 -> externo        
        public TCurso Curso { get; set; }
    }
}
