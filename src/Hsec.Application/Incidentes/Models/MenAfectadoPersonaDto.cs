using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;

namespace Hsec.Application.Incidentes.Models
{ 
    public class MenAfectadoPersonaDto : IMapFrom<TDiasPerdidosAfectado>
    {
        public string CodTipAccidente { get; set; }
        public string PeridoAnio { get; set; }
        public string PeridoMes { get; set; }
        public string CantidadDias { get; set; }
        public string CodGrado { get; set; }
        public string Correlativo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MenAfectadoPersonaDto, TDiasPerdidosAfectado>()
                .ForMember(t => t.Tipo , opt => opt.MapFrom(t => t.CodGrado))
                .ForMember(t => t.Correlativo, opt => opt.MapFrom(t => t.Correlativo));
            profile.CreateMap<TDiasPerdidosAfectado, MenAfectadoPersonaDto>()
                .ForMember(t => t.CodGrado, opt => opt.MapFrom(t => t.Tipo))
                .ForMember(t => t.Correlativo, opt => opt.MapFrom(t => t.Correlativo));
        }
    }
}