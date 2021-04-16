using Hsec.Domain.Common;
using System;

namespace Hsec.Domain.Entities.Verficaciones
{
    public class TVerificaciones : AuditableEntity
    {
        public string CodVerificacion {get;set;}
        public string CodTabla {get;set;}
        public string CodPosicionGer {get;set;}
        public string CodPosicionSup {get;set;}
        public string CodAreaHSEC {get;set;}
        public string CodTipoVerificacion {get;set;}
        public string CodNivelRiesgo {get;set;}
        public string CodVerificacionPor {get;set;}
        public DateTime FechaVerificacion {get;set;}
        public string HoraVerificacion {get;set;}
        public string CodUbicacion {get;set;}
        public string CodSubUbicacion {get;set;}
        public string CodUbicacionEspecifica {get;set;}
        public string DesUbicacion {get;set;}
        public string Dispositivo {get;set;}

        
    }
}
