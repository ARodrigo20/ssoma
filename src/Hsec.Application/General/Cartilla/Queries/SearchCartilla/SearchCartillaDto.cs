using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Cartilla.Queries.SearchCartilla
{
    public class SearchCartillaDto : IMapFrom<TCartilla>
    {
        public string CodCartilla { get; set; }
        public string DesCartilla { get; set; }
        public string TipoCartilla { get; set; }
        public string PeligroFatal { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TCartilla, SearchCartillaDto>()
            .ForMember(t => t.DesCartilla , opt => opt.AllowNull());
            
        }
    }
}