using System.Collections.Generic;

namespace Hsec.Application.Incidentes.Queries.GetBuscarInsidentes
{
  public class BuscarAuditoriaVM
  {
    public ICollection<BuscarAuditoriaDto> List {get;set;}
    public int Count {get;set;}
    public int Pagina { get; set; }
  }
}