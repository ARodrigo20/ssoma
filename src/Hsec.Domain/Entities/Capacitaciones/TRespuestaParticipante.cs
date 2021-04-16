using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TRespuestaParticipante : AuditableEntity
    {                
        public string CodCurso { get; set; }
        public string CodPersona { get; set; }
        public int CodPregunta { get; set; }
        public decimal? Nota { get; set; }
        public string Respuesta { get; set; }      
    }
}
