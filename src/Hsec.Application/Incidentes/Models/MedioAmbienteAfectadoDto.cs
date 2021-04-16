using Hsec.Domain.Entities.Incidentes;
using Hsec.Application.Common.Mappings;
using AutoMapper;

namespace Hsec.Application.Incidentes.Models
{
    public class MedioAmbienteAfectadoDto : IMapFrom<TAfectadoMedioAmbiente>
    {
        public string CodImpAmbiental { get; set; }
        public string Descripcion { get; set; }
        public int Correlativo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MedioAmbienteAfectadoDto, TAfectadoMedioAmbiente>();
            profile.CreateMap<TAfectadoMedioAmbiente, MedioAmbienteAfectadoDto>();
        }
    }
}