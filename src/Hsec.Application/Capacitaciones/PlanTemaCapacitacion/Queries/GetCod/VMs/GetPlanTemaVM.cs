using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetCod.VMs
{
    public class GetPlanTemaVM : IMapFrom<TPlanTema>
    {
        public string codTemaCapacita { get; set; }
        public bool tipo { get; set; }
        public string codReferencia { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TPlanTema, GetPlanTemaVM>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));    
        }
    }
}
