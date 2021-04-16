using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries.VMs
{
    public class GetCursoYCursoPosicionVM
    {
        public GetCursoYCursoPosicionVM() {
            curso = new List<CursoDto>();
            cursoPosicion = new List<CursoPosicionDto>();
        }
        public IList<CursoDto> curso { get; set; }
        public IList<CursoPosicionDto> cursoPosicion { get; set; }


    }
}
