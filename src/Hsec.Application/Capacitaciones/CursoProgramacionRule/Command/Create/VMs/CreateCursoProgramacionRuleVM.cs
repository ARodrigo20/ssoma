using Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Create.DTOs;
using Hsec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Create.VMs
{
    public class CreateCursoProgramacionRuleVM
    {
        public CreateCursoProgramacionRuleVM()
        {
            data = new List<CreateCursoProgramacionRuleDto>();
        }
        public IList<CreateCursoProgramacionRuleDto> data { get; set; }
    }
}
