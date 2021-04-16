using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs
{
    public class GetCursoProgramacionFechasPartVM
    {
        //GetCursoProgramacionFechasPartDto

        public GetCursoProgramacionFechasPartVM()
        {
            data = new List<GetCursoProgramacionFechasPartDto>();

        }

        public IList<GetCursoProgramacionFechasPartDto> data { get; set; }
        public int count { get; set; }
    }
}


