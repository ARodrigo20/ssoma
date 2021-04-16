using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.ControlCritico.Queries.SearchControlCritico
{
    public class SearchControlCriticoDto : IMapFrom<TControlCritico>
    {
        public string CodCC { get; set; }
        public string CodRiesgo { get; set; }
        public string DesCC { get; set; }
        public string CodTipoCC { get; set; }
        public string CodPeligroFatal { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TControlCritico, SearchControlCriticoDto>()
                .ForMember(t => t.CodTipoCC, opt => opt.MapFrom(t => t.TipoCC))
                .ForMember(t => t.CodPeligroFatal, opt => opt.MapFrom(t => t.PeligroFatal));
            
        }
    }
}