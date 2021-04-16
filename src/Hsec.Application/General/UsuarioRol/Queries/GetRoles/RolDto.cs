using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System;
using System.Collections.Generic;

namespace Hsec.Application.General.Roles.Queries.GetRoles
{
  public class RolDto
  {
    public string CodRol { get; set; }
    public string Descripcion { get; set; }
    public void Mapping(Profile profile)
        {
            profile.CreateMap<RolDto, TRol>();
            profile.CreateMap<TRol, RolDto>();
        }
  }
}