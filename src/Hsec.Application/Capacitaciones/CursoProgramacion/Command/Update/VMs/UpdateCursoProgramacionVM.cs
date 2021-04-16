using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Update.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Command.Update.VMs
{
    public  class UpdateCursoProgramacionVM
    {
        public UpdateCursoProgramacionVM()
        {
            data = new List<UpdateCursoProgramacionDto>();
        }
        public IList<UpdateCursoProgramacionDto> data { get; set; }
        public int count { get; set; }
    }
}
