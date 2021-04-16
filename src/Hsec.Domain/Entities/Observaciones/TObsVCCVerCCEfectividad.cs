using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObsVCCVerCCEfectividad: AuditableEntity
    {
        public int Correlativo {get;set;}
        public string CodVcc {get;set;}
        public string CodCartilla {get;set;}
        public string CodCC {get;set;}
        public string Efectividad {get;set;}

    }
}