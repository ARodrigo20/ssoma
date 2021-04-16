using Hsec.Application.Capacitaciones.Preguntas_Mis_Capacitaciones.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_Mis_Capacitaciones.Queries.VMs
{
    public class GetMisCapacitacionesVM
    {
        public GetMisCapacitacionesVM()
        {
            data = new List<GetMisCapacitacionesDTO>();
        }

        public IList<GetMisCapacitacionesDTO> data { get; set; }
        public int count { get; set; }
    }
}
