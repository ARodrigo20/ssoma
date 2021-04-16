using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;

namespace Hsec.Application.Observacion.Queries.GetObservacion
{
    public class PersonaObservadaDto : IMapFrom<TObsTaPersonaObservada>
    {
        public string CodPersonaMiembro { get; set; }
        public string CodObservacion { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TObsTaPersonaObservada,PersonaObservadaDto > ();
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }
    }
}