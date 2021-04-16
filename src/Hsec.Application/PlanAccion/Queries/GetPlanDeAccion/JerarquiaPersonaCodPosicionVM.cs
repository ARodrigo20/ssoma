using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class JerarquiaPersonaCodPosicionVM
    {
        public JerarquiaPersonaCodPosicionVM() {
            data = new List<JerarquiaPersonaCodPosicionDto>();
        }
        public IList<JerarquiaPersonaCodPosicionDto> data { get; set; }
        public int count { get; set; }
    }
}
