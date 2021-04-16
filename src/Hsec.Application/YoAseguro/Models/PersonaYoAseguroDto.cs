using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.YoAseguro;

namespace Hsec.Application.YoAseguro.Models
{
    public class PersonaYoAseguroDto : IMapFrom<TPersonaYoAseguro>
    {
        public string CodYoAseguro { get; set; }
        public string CodPersona { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TPersonaYoAseguro, PersonaYoAseguroDto>();
            profile.CreateMap<PersonaYoAseguroDto, TPersonaYoAseguro>();
        }
    }
}
