using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.PlanAccion
{
    public class TValidadorArchivo : AuditableEntity
    {
        public int Correlativo { get; set; }
        public string NroDocReferencia { get; set; }
        public string CodPersona { get; set; }
        public int CodArchivo { get; set; }
        public int? EstadoAccion { get; set; }
    }
}
