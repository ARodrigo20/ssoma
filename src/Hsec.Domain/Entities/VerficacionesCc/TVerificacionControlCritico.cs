using Hsec.Domain.Common;
using System;

namespace Hsec.Domain.Entities.VerficacionesCc
{
    public class TVerificacionControlCritico : AuditableEntity
    {
        public string CodigoVCC { get; set; }
	    public DateTime Fecha { get; set; }
	    public string CodResponsable { get; set; }
        public string Gerencia { get; set; }
        public string SuperIndendecnia { get; set; }
	    public string Empresa { get; set; }
	    public string Cartilla { get; set; }
    }
}