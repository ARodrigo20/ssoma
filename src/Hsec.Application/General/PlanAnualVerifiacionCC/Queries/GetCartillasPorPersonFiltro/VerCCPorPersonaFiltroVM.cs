using System.Collections.Generic;

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPersonFiltro
{
  public class VerCCPorPersonaFiltroVM
  {
    public ICollection<VerCCProDto> list {get;set;}
    public int Count { get; set; }
    public int Pagina { get; set; }
  }
}