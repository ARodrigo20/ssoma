using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries.VMs
{
    public class JerarquiaNodeVM
    {
        public JerarquiaNodeVM()
        {
            children = new List<JerarquiaNodeVM>();
        }
        public string label { get; set; }
        public int? data { get; set; }
        public string Tipo { get; set; }
        public string CodElipse { get; set; }
        public IList<JerarquiaNodeVM> children { get; set; }
    }
}
