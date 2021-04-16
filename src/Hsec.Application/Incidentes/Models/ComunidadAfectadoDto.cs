using Hsec.Domain.Entities.Incidentes;
using Hsec.Application.Common.Mappings;
using AutoMapper;

namespace Hsec.Application.Incidentes.Models
{
    public class ComunidadAfectadoDto : IMapFrom<TAfectadoComunidad>
    {
        public string CodComuAfec { get; set; }
        public string CodMotivo { get; set; }
        public string Descripcion { get; set; }
        public int Correlativo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ComunidadAfectadoDto, TAfectadoComunidad>();
            profile.CreateMap<TAfectadoComunidad, ComunidadAfectadoDto>();
        }

    }
}