using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create.DTOs
{
    public class CreateCursoProgramacionExpositorDto : IMapFrom<TExpositor>
    {
        public string codPersona { get; set; }
        //public string codTemaCapacita { get; set; }
        public bool tipo { get; set; } // 1 -> interno y 0 -> externo
        public ExpositorExternoCursoProgramacionVM personaExterna { get; set; }
        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<TExpositor, CreateCursoProgramacionExpositorDto>()
        //    .ForMember(i=>i.codTemaCapacita,opt => opt.MapFrom(t => t.CodCurso));    
        //}
    }
}
