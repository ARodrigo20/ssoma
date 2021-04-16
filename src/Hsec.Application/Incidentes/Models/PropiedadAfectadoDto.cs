using Hsec.Domain.Entities.Incidentes;
using Hsec.Application.Common.Mappings;
using AutoMapper;

namespace Hsec.Application.Incidentes.Models
{
    public class PropiedadAfectadoDto : IMapFrom<TAfectadoPropiedad>
    {
        public string CodTipActivo { get; set; }
        public string CodActivo { get; set; }
        public string Operador { get; set; }
        public string Dano { get; set; }
        public string CodCosto { get; set; }
        public string Monto { get; set; }
        public string Descripcion { get; set; }
        public int Correlativo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PropiedadAfectadoDto, TAfectadoPropiedad>()
                .ForMember(m => m.Daño, opt => opt.MapFrom(f => f.Dano));
            profile.CreateMap<TAfectadoPropiedad, PropiedadAfectadoDto>()
                .ForMember(m => m.Dano, opt => opt.MapFrom(f => f.Daño));
        }
    }
}