using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion
{
    public class PersonaObservadaDto : IMapFrom<TObsTaPersonaObservada>
    {
        public string CodPersonaMiembro { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonaObservadaDto, TObsTaPersonaObservada>();
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }
    }
}
