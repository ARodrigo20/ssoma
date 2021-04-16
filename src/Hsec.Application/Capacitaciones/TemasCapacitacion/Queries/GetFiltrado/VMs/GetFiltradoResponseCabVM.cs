using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado.VMs
{
    public class GetFiltradoResponseCabVM
    {
        public GetFiltradoResponseCabVM()
        {
            data = new List<GetFiltradoResponseVM>();
        }

        public IList<GetFiltradoResponseVM> data { get; set; }
        public int count { get; set; }
    }
}
