using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObsVCCRespuesta: AuditableEntity
    {
        public int CodRespuesta {get;set;}
        public string CodigoCriterio {get;set;}
        public string CodigoCC {get;set;}
        public string CodVcc {get;set;}
        public string Respuesta {get;set;}
        public string Justificacion {get;set;}
        public string AccionCorrectiva {get;set;}
    }
}