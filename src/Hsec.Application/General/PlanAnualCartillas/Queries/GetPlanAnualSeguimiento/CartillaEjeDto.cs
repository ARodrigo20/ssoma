using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.PlanAnualCartillas.Queries.GetPlanAnualSeguimiento
{
    public class CartillaEjeDto
    {
        public string Codigo { get; set; }
        public int Ejecutados { get; set; }
        public int Planeados { get; set; }
        public CartillaEjeDto(string codigo, int ejecutados, int planeados)
        {
            Codigo = codigo;
            Ejecutados = ejecutados;
            Planeados = planeados;
        }
        public CartillaEjeDto(string codigo, string planeados)
        {
            Codigo = codigo;
            Planeados = Int32.Parse(planeados);
        }
    }
}