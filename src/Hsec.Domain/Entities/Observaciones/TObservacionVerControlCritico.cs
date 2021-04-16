using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObservacionVerControlCritico: AuditableEntity
    {
        public string CodVcc {get;set;}
        public string CodCartilla {get;set;}
        //public DateTime Fecha {get;set;}
        public string CodObservacion {get;set;}
        public string CodObservadoPor {get;set;}
        public string CodPosicionGer {get;set;}
        public string TareaObservada {get;set;}
        public string Empresa {get;set;}
    }
}