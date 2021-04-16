using System.Collections.Generic;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Roles.Queries.GetRolPerfil
{
  public class GetRolPerfilVM
  {
    public string Descripcion { get; set; }
    public string CodRol {get;set;}
    public IList<AccesosDto> Permisos { get; set; }

  }
}

