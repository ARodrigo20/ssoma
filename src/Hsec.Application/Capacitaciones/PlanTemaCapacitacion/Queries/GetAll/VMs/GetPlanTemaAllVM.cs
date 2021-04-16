using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetAll.DTOs;
using Hsec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetAll.VMs
{
    public class GetPlanTemaAllVM
    {
        public GetPlanTemaAllVM() {
            data = new List<GetPlanTemaAllDto>();
        }
        public IList<GetPlanTemaAllDto> data { get; set; }
        public int count { get; set; }
    }
}
