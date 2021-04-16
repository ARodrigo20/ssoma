using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPersonaBuscar
{
    public class DetalleAfectadoDto : IMapFrom<TIncidente>
    {
        public string TipoAfectado { get; set; }
        public string Afectado { get; set; }
        public string ZonasLesion { get; set; }
        public string MecanismoLesion {get;set;}
        public string NatLesion {get;set;}
        public string Experiencia {get;set;}
        public string DesExperiencia {get;set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TInvestigaAfectado, DetalleAfectadoDto>()
                .ForMember(d => d.TipoAfectado, opt => opt.MapFrom(t => Domain.Enums.TipoAfectado.Persona.ToString()))
                .ForMember(d => d.Afectado, opt => opt.MapFrom(per => per.DocAfectado))
                .ForMember(d => d.ZonasLesion, opt => opt.MapFrom(per => per.ZonasDeLesion))
                .ForMember(d => d.MecanismoLesion, opt => opt.MapFrom(per => per.CodMecLesion))
                .ForMember(d => d.NatLesion, opt => opt.MapFrom(per => per.CodNatLesion))
                .ForMember(d => d.Experiencia, opt => opt.MapFrom(per => per.CodExperiencia))
                .ForMember(d => d.DesExperiencia, opt => opt.MapFrom(per => per.DesExperiencia));

        }
    }
}