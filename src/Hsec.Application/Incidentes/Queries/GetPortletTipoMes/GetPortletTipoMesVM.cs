using System.Collections.Generic;

namespace Hsec.Application.Incidentes.Queries.GetPortletTipoMes
{
    public class GetPortletTipoMesVM 
    {
        IList<PortletTipoIncidente> List { get; set; }
    }

    public class PortletTipoIncidente
    {
        public string CodTipoIncidente { get; set; }
        public int Total { get; set; }
    }
}