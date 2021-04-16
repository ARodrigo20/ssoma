using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using Hsec.Domain.Enums;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectado
{
    public class DetalleAfectadoDto : IMapFrom<TIncidente>
    {
        public TipoAfectado tipoAfectado { get; set; }
        public string CodIncidente { get; set; }
        public string Correlativo { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {

            profile.CreateMap<TAfectadoComunidad, DetalleAfectadoDto>()
                .ForMember(d => d.tipoAfectado, opt => opt.MapFrom(t => TipoAfectado.Comunidad));
            profile.CreateMap<TAfectadoMedioAmbiente, DetalleAfectadoDto>()
                .ForMember(d => d.tipoAfectado, opt => opt.MapFrom(t => TipoAfectado.Medio_Ambiente));
            profile.CreateMap<TAfectadoPropiedad, DetalleAfectadoDto>()
                .ForMember(d => d.tipoAfectado, opt => opt.MapFrom(t => TipoAfectado.Propiedad));
            profile.CreateMap<TInvestigaAfectado, DetalleAfectadoDto>()
                .ForMember(d => d.tipoAfectado, opt => opt.MapFrom(t => TipoAfectado.Persona))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(t => t.DocAfectado));

        }
    }
}