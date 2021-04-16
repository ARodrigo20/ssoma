using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoBuscarMedioAmbiente
{
    public class DetalleAfectadoDto : IMapFrom<TIncidente>
    {
        public string TipoAfectado { get; set; }
        public string Impacto {get;set;}
        public string MedioAmbienteDescripcion {get;set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAfectadoMedioAmbiente, DetalleAfectadoDto>()
                .ForMember(d => d.TipoAfectado, opt => opt.MapFrom(t => Domain.Enums.TipoAfectado.Medio_Ambiente.ToString()))
                .ForMember(d => d.Impacto, opt => opt.MapFrom(ma => ma.CodImpAmbiental))
                .ForMember(d => d.MedioAmbienteDescripcion, opt => opt.MapFrom(ma => ma.Descripcion));
        }
    }
}