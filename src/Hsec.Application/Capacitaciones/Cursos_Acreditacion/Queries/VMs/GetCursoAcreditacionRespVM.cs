using Hsec.Application.Capacitaciones.Cursos_Acreditacion.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Cursos_Acreditacion.Queries.VMs
{
    public class GetCursoAcreditacionRespVM
    {
        public GetCursoAcreditacionRespVM() {
            data = new List<GetCursoAcreditacionRespDTO>();        
        }

        public IList<GetCursoAcreditacionRespDTO> data { get; set; }
        public int count { get; set; }
    }
}