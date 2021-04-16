using Hsec.Application.Capacitaciones.CursosFuturos.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursosFuturos.Queries.VMs
{
    public class GetCursosFuturosVM
    {
        public GetCursosFuturosVM() {
            data = new List<GetCursosFuturosDTO>();
        }
        public IList<GetCursosFuturosDTO> data { get; set; }
        public int count { get; set; }
    }
}
