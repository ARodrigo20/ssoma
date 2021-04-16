using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create.VMs
{
    public class CreateCursoProgramacionVM
    {
        public CreateCursoProgramacionVM()
        {
            data = new List<CreateCursoProgramacionDto>();
        }
        public IList<CreateCursoProgramacionDto> data { get; set; }       
    }
}
