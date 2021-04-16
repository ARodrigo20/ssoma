using System;

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetPlanAnualSeguimientoTotal
{
  public class ModuloEjeDto
  {
    public string Codigo { get; set; }
    public int Ejecutados { get; set; }
    public int Planeados { get; set; }

    public ModuloEjeDto(string codigo, int ejecutados, int planeados)
    {
      Codigo = codigo;
      Ejecutados = ejecutados;
      Planeados = planeados;
    }
    public ModuloEjeDto(string codigo, string planeados)
    {
      Codigo = codigo;
      Planeados = Int32.Parse(planeados);
    }
  }
}