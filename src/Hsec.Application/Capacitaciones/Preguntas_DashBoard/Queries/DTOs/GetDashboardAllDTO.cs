using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries.DTOs
{
    public class GetDashboardAllDTO
    {
        public GetDashboardAllDTO()
        {
            data = new List<GetDashboardDTO>();
        }

        public IList<GetDashboardDTO> data { get; set; }
        public int count { get; set; }
    }
}
