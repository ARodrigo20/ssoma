using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.PlanAccion.Queries.GetPorlet
{
    public class PlanesDto
    {
        public int CodAccion { get; set; }
        public string DocReferencia { get; set; }
        public string TablaRef { get; set; }
        public string Tarea { get; set; }
        public string Estado { get; set; }
        public DateTime FechaFinal { get; set; }
        
    }
}
