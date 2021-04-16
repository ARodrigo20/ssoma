using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Verficaciones
{
    public class TVerificacionIPERC: AuditableEntity
    {
        public string CodVerificacion {get;set;} 
				public int Correlativo {get;set;}
				public string CodTabla {get;set;}
				public string CondObservada {get;set;} 
				public string CondAccionInmediata {get;set;} 
				public string StopWork {get;set;}
    }
}
