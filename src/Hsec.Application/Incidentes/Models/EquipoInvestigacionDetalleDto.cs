using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;

namespace Hsec.Application.Incidentes.Models
{
    public class EquipoInvestigacionDetalleDto : IMapFrom<TEquipoInvestigacion>
    {
        public string CodPersona { get; set; }
        public bool Lider { get; set; }
        public string AreaDes {get;set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EquipoInvestigacionDetalleDto, TEquipoInvestigacion>();
            profile.CreateMap<TEquipoInvestigacion, EquipoInvestigacionDetalleDto>()
                .ForMember(t => t.Lider, opt => opt.MapFrom(t => t.Lider.ToUpper().Equals("TRUE")));
        }
    }
}