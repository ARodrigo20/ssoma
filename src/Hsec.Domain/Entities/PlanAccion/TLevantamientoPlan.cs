using Hsec.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hsec.Domain.Entities.PlanAccion
{
    public class TLevantamientoPlan : AuditableEntity
    {
        public int CodAccion { get; set; }
        public string CodPersona { get; set; }
        public int Correlativo { get; set; }
        public string Descripcion { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? Fecha { get; set; }
        public double PorcentajeAvance { get; set; }
        //public string EstadoMejora { get; set; }   
        public TAccion Accion { get; set; }
        public bool Rechazado { get; set; }
    }
}
