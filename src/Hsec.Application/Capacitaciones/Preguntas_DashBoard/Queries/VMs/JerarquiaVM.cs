using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries.VMs
{
    public class JerarquiaVM
    {
        public JerarquiaVM()
        {
            data = new List<JerarquiaNodeVM>();
        }
        public IList<JerarquiaNodeVM> data { get; set; }
        public int count { get; set; }
    }
}
