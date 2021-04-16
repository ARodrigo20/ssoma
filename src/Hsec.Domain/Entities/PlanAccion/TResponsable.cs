using Hsec.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Hsec.Domain.Entities.PlanAccion
{
    public class TResponsable : AuditableEntity
    {
        public int CodAccion { get; set; }
        [MaxLength(20)]
        public string? CodPersona { get; set; }
        public TAccion Accion { get; set; }
    }
}
