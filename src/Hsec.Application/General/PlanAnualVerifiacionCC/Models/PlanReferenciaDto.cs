using Hsec.Domain.Enums;

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Models
{
    public class PlanReferenciaDto {
        public PlanReferenciaDto(){}
        public PlanReferenciaDto (string codReferencia, string valor) {
            this.CodReferencia = codReferencia;
            this.Valor = valor;
        }
        public string CodReferencia { get; set; }
        public string Valor { get; set; }

    }
}