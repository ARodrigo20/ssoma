using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TAprobacionPlan : AuditableEntity
    {
        public TAprobacionPlan()
        {
        }

        public int CodAprobacion { get; set; }
        public int CodAccion { get; set; }
        public string DocReferencia { get; set; }
        public string EstadoDoc { get; set; }
        public string CodTabla { get; set; }
    }
}