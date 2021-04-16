using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoComunidadBuscar
{
    public class DetalleAfectadoDto : IMapFrom<TIncidente>
    {
        public string TipoAfectado { get; set; }
        public string Comunidad {get;set;}
        public string Motivo {get;set;}
        public string ComuDescripcion {get;set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAfectadoComunidad, DetalleAfectadoDto>()
                .ForMember(d => d.TipoAfectado, opt => opt.MapFrom(t => Domain.Enums.TipoAfectado.Comunidad.ToString()))
                .ForMember(d => d.Comunidad, opt => opt.MapFrom(com => com.CodComuAfec))
                .ForMember(d => d.Motivo, opt => opt.MapFrom(com => com.CodMotivo))
                .ForMember(d => d.ComuDescripcion, opt => opt.MapFrom(com => com.Descripcion));
        }
    }
}