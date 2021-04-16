using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.DTOs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.VMs
{
    public class GetCursoProgramacionRuleResponseAllVM : IMapFrom<TCursoRules>
    {
        public GetCursoProgramacionRuleResponseAllVM() {
            data = new List<GetCursoProgramacionRuleResponseAllDto>();        
        }

        public IList<GetCursoProgramacionRuleResponseAllDto> data { get; set; }
        public int count { get; set; }
    }
}
