using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Otros;

namespace Hsec.Application.Reunion.Models
{
    public class TAusentesReunionDto : IMapFrom<TAusentesReunion>
    {
        public string CodReunion { get; set; }

        public string CodPersona { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAusentesReunionDto, TAusentesReunion>();
            profile.CreateMap<TAusentesReunion, TAusentesReunionDto>();
        }
    }
}
