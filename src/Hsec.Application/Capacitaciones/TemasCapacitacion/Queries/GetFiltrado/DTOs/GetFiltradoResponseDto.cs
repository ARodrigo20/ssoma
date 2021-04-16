using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado.DTOs
{
    public class GetFiltradoResponseDto : IMapFrom<TTemaCapEspecifico>
    {
        public int correlativo { get; set; }
        public string codTemaCapacita { get; set; }
        public string codPeligro { get; set; }
        public string codRiesgo { get; set; }
        public bool estado { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TTemaCapEspecifico, GetFiltradoResponseDto>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));    
        }
    }
}
