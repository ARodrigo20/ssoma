using System.Collections.Generic;

namespace Hsec.Application.Observacion.Queries.GetResueltosVerCC
{
  public class ResueltosVM
  {
    public ResueltosVM()
    {
      list = new List<CartillaDto>();
    }

    public ICollection<CartillaDto> list {get;set;}
    
    
  }
}