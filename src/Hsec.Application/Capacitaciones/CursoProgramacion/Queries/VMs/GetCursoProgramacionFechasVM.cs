using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs
{
    public class GetCursoProgramacionFechasVM
    {
        public GetCursoProgramacionFechasVM() {
            data = new List<GetCursoProgramacionFechasDto>();        
        }
        public IList<GetCursoProgramacionFechasDto> data { get; set; }
        public int count { get; set; }
    }
}
