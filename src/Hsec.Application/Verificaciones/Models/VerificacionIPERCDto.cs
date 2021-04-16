using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Verficaciones;
using Hsec.Domain.Enums;
namespace Hsec.Application.Verificaciones.Models
{
    public class VerificacionIPERCDto : IMapFrom<TVerificacionIPERC>
    {
        public string CodVerificacion {get;set;} 
		// public int Correlativo {get;set;}
		public string CodTabla {get;set;}
		public string CondObservada {get;set;} 
		public string CondAccionInmediata {get;set;} 
		public string StopWork {get;set;}
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VerificacionIPERCDto, TVerificacionIPERC>();
            profile.CreateMap<TVerificacionIPERC, VerificacionIPERCDto>();
        }
    }
}