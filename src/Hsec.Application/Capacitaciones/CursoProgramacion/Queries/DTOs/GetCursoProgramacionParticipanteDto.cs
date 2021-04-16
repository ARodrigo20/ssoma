using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs
{
    public class GetCursoProgramacionParticipanteDto : IMapFrom<TParticipantes>
    {
        public string codPersona { get; set; } // Primary Key 1
        public string codCurso { get; set; } // Primary Key 2
        public string? nota { get; set; }
        public bool tipo { get; set; } // Si es participante invitado o colado !!!
        public string nombre { get; set; }
        public string dni { get; set; }
        public string empresa { get; set; }
        public int intentos { get; set; }
        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<TParticipantes, GetCursoProgramacionParticipanteDto>()
        //    .ForMember(i=>i.codTemaCapacita,opt => opt.MapFrom(t => t.CodCurso));    
        //}
    }
}
