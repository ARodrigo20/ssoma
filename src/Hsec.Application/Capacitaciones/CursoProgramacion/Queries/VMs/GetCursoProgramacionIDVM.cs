using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs
{
    public class GetCursoProgramacionIDVM
    {
        public GetCursoProgramacionIDVM()
        {
            data = new List<GetCursoProgramacionIDDto>();
        }
        public IList<GetCursoProgramacionIDDto> data { get; set; }
        public int count { get; set; }
    }
}
