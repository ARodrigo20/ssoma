using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoBuscar
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
        public string Operador {get;set;}
        public string PropDescripcion {get;set;}
        public string Costo {get;set;}
        public string Monto {get;set;}
        public string TipoActivo {get;set;}
        public string Comunidad {get;set;}
        public string Motivo {get;set;}
        public string ComuDescripcion {get;set;}
        public string Impacto {get;set;}
        public string MedioAmbienteDescripcion {get;set;}

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

            profile.CreateMap<TAfectadoPropiedad, DetalleAfectadoDto>()
                .ForMember(d => d.TipoAfectado, opt => opt.MapFrom(t => Domain.Enums.TipoAfectado.Propiedad.ToString()))
                .ForMember(d => d.Operador, opt => opt.MapFrom(pro => pro.Descripcion))
                .ForMember(d => d.PropDescripcion, opt => opt.MapFrom(pro => pro.Descripcion))
                .ForMember(d => d.Costo, opt => opt.MapFrom(pro => pro.Descripcion))
                .ForMember(d => d.Monto, opt => opt.MapFrom(pro => pro.Descripcion))
                .ForMember(d => d.TipoActivo, opt => opt.MapFrom(pro => pro.Descripcion));

            profile.CreateMap<TAfectadoComunidad, DetalleAfectadoDto>()
                .ForMember(d => d.TipoAfectado, opt => opt.MapFrom(t => Domain.Enums.TipoAfectado.Comunidad.ToString()))
                .ForMember(d => d.Comunidad, opt => opt.MapFrom(com => com.CodComuAfec))
                .ForMember(d => d.Motivo, opt => opt.MapFrom(com => com.CodMotivo))
                .ForMember(d => d.ComuDescripcion, opt => opt.MapFrom(com => com.Descripcion));

            profile.CreateMap<TAfectadoMedioAmbiente, DetalleAfectadoDto>()
                .ForMember(d => d.TipoAfectado, opt => opt.MapFrom(t => Domain.Enums.TipoAfectado.Medio_Ambiente.ToString()))
                .ForMember(d => d.Impacto, opt => opt.MapFrom(ma => ma.CodImpAmbiental))
                .ForMember(d => d.MedioAmbienteDescripcion, opt => opt.MapFrom(ma => ma.Descripcion));

        }
    }
}