using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs
{
    public class GetCursoProgramacionFiltroGenVM
    {
        public GetCursoProgramacionFiltroGenVM() {
            data = new List<GetCursoProgramacionFiltroGenDto>();
        }
        public IList<GetCursoProgramacionFiltroGenDto> data { get; set; }
        public int count { get; set; }
    }
}