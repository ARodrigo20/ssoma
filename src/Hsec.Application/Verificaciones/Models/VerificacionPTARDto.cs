using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Verficaciones;
using Hsec.Domain.Enums;
namespace Hsec.Application.Verificaciones.Models
{
    public class VerificacionPTARDto : IMapFrom<TVerificacionPTAR>
    {
        public string CodVerificacion {get;set;}
		// public int Correlativo {get;set;}
		public string CodTabla {get;set;}
		public string CondObservada {get;set;}
		public string CondAccionInmediata {get;set;}
		public string StopWork {get;set;}
        public bool P1_IdentificoRelacionados { get; set; }
        public bool P2_ControlesImplementados { get; set; }
        public bool P3_ReviseElContenido { get; set; }
        public bool P4_CorrespondeAlEjecutado { get; set; }
        public bool P5_NoSeEjecutaElTrabajo { get; set; }
        public bool P6_RevisadoyFirmado { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VerificacionPTARDto, TVerificacionPTAR>();
            profile.CreateMap<TVerificacionPTAR, VerificacionPTARDto>();
        }
    }
}