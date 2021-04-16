using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TAlternativas : AuditableEntity
    {
        public int CodAlternativa { get; set; }
        public int CodPregunta { get; set; }
        public string Descripcion { get; set; }
        public TPreguntas Preguntas { get; set; }
    }
}
