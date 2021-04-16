using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObsVCCCierreIteraccion: AuditableEntity
    {
        public int CodCierreIt {get;set;}
        public string CodVcc {get;set;}
        public string CodDesCierreIter {get;set;}
        public string Respuesta {get;set;}
    }
}