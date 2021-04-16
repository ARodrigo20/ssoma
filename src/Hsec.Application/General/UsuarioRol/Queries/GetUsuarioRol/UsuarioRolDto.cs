using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System;
using System.Collections.Generic;

namespace Hsec.Application.General.UsuarioRol.Queries.GetUsuarioRol
{
    public class UsuarioRolDto : IMapFrom<TUsuarioRol>
    {
        public int CodUsuario { get; set; }
        public int CodRol { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UsuarioRolDto, TUsuarioRol>();
            profile.CreateMap<TUsuarioRol, UsuarioRolDto>();
        }
    }
}
