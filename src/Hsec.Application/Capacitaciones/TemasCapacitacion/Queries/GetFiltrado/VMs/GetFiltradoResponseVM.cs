using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado.DTOs;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado.VMs
{
    public class GetFiltradoResponseVM : IMapFrom<TTemaCapacitacion>
    {
        public GetFiltradoResponseVM() 
        {
            temaCapEspecifico = new List<GetFiltradoResponseDto>();
        }

        public string codTemaCapacita { get; set; } //identity
        public string codTipoTema { get; set; }
        public string codAreaCapacita { get; set; }
        public string descripcion { get; set; }
        public string competenciaHs { get; set; }
        public string codHha { get; set; }
        public bool estado { get; set; }
        public IList<GetFiltradoResponseDto> temaCapEspecifico { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TTemaCapacitacion, GetFiltradoResponseVM>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));    
        }
    }
}
