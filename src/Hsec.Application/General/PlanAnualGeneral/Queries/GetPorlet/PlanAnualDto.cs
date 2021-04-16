using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetPorlet
{
    public class PlanAnualDto
    {
        public string Codigo { get; set; }
        public int Ejecutados { get; set; }
        public int Planeados { get; set; }

        public PlanAnualDto(string codigo, int ejecutados, int planeados)
        {
            Codigo = codigo;
            Ejecutados = ejecutados;
            Planeados = planeados;
        }
    }
}
