using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class JerarquiaPersonaCodPosicionDto
    {
        public string codPersona { get; set; }
        public string path { get; set; }
        public int? codPosicion { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
    }
}
