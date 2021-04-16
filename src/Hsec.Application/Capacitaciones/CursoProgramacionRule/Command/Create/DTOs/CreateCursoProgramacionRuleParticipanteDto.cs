using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Create.DTOs
{
    public class CreateCursoProgramacionRuleParticipanteDto : IMapFrom<TParticipantes>
    {
        public string codPersona { get; set; } // Primary Key 1
        public string codTemaCapacita { get; set; } // Primary Key 2
        public decimal? nota { get; set; }
        public bool tipo { get; set; } // Si es participante invitado o colado !!!
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TParticipantes, CreateCursoProgramacionRuleParticipanteDto>()
            .ForMember(i => i.codTemaCapacita, opt => opt.MapFrom(t => t.CodCurso));
        }
    }
}
