using Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries.VMs
{
    public class GetDashboardVM
    {
        public GetDashboardVM() {
            data = new List<GetDashboardDTO>();
        }

        public IList<GetDashboardDTO> data { get; set; }       
        public int count { get; set; }
    }
}
