using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Command.Update.DTOs
{
    public class UpdateCursoProgramacionParticipanteDto
        //: IMapFrom<TParticipantes>
    {
        public string codCurso { get; set; }
        public string codPersona { get; set; } // Primary Key 1
        //public string codTemaCapacita { get; set; } // Primary Key 2
        public decimal? nota { get; set; }
        public bool tipo { get; set; } // Si es participante invitado o colado !!!
        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<TParticipantes, UpdateCursoProgramacionParticipanteDto>()
        //    .ForMember(i=>i.codTemaCapacita,opt => opt.MapFrom(t => t.CodCurso));    
        //}
    }
}
