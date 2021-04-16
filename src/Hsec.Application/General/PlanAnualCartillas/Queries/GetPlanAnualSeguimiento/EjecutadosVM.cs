using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.PlanAnualCartillas.Queries.GetPlanAnualSeguimiento
{
    public class EjecutadosVM
    {
        EjecutadosVM()
        {
            list = new List<EjecutadoDto>();
        }
        public List<EjecutadoDto> list { get; set; }
    }

    public class EjecutadoDto
    {
        public string codPersona { get; set; }
        public string codCartilla { get; set; }
        public int valor { get; set; }
    }

}