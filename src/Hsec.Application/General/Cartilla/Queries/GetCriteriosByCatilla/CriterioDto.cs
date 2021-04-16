using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Cartilla.Queries.GetCriteriosByCatilla
{
    public class CriterioDto : IMapFrom<TCriterio>
    {
        public string CodCrit { get; set; }
        public string ControlCritico { get; set; }
        public string CodCC { get; set; }
        public string Criterio { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TCriterio, CriterioDto>();
            profile.CreateMap<CriterioDto, TCriterio>();
        }
    }
}