using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs
{
    public class GetCursoProgramacionVM
    {
        public GetCursoProgramacionVM() {
            data = new List<GetCursoProgramacionDto>();
        }
        public IList<GetCursoProgramacionDto> data { get; set; }
        public int count { get; set; }
    }
}
