using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System.Collections.Generic;

namespace Hsec.Application.General.ControlCritico.Models
{
    public class ControlCriticoDto : IMapFrom<TControlCritico>
    {
        public string CodCC { get; set; }
        public string CodRiesgo { get; set; }
        public string DesCC { get; set; }
        public string CodTipoCC { get; set; }
        public string CodPeligroFatal { get; set; }
        public string Modulo { get; set; }
        public ICollection<CriterioDto> Criterios { get; set; }

        public ControlCriticoDto()
        {
            Criterios = new HashSet<CriterioDto>();
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TControlCritico, ControlCriticoDto>()
                .ForMember(t => t.CodTipoCC , opt => opt.MapFrom(t => t.TipoCC))
                .ForMember(t => t.CodPeligroFatal , opt => opt.MapFrom(t => t.PeligroFatal));
            profile.CreateMap<ControlCriticoDto, TControlCritico>()
                .ForMember(t => t.TipoCC , opt => opt.MapFrom(t => t.CodTipoCC))
                .ForMember(t => t.PeligroFatal , opt => opt.MapFrom(t => t.CodPeligroFatal));
        }
    }
}