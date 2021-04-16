using System.Collections.Generic;

namespace Hsec.Application.General.Roles.Queries.GetRoles
{
  public class RolVM
  {
    public IList<RolDto> list { get; set; }
    public int Count { get; set; }
  }
}