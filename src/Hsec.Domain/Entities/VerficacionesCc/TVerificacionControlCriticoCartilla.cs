using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.VerficacionesCc
{
    public class TVerificacionControlCriticoCartilla : AuditableEntity
    {
		public string CodigoVCC { get; set; }
		public string CodCartilla { get; set; }
		public string CodCC { get; set; }
		// public string CC { get; set; }
		public string Cumplimiento { get; set; }
		public string Efectividad { get; set; }
		public string Justificacion { get; set; }
    }
}