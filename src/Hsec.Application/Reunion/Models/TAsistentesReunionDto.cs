using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Otros;

namespace Hsec.Application.Reunion.Models
{
    public class TAsistentesReunionDto : IMapFrom<TAsistentesReunion>
    {
        public string CodReunion { get; set; }

        public string CodPersona { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAsistentesReunionDto, TAsistentesReunion>();
            profile.CreateMap<TAsistentesReunion, TAsistentesReunionDto>();
        }
    }
}
