using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.VMs
{
    public class GetCursoProgramacionRuleVM
    {
        public GetCursoProgramacionRuleVM() {
            data = new List<GetCursoProgramacionRuleDto>();
        }
        public IList<GetCursoProgramacionRuleDto> data { get; set; }
        public int count { get; set; }        
    }
}
