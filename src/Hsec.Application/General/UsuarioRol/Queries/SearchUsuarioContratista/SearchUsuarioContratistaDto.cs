using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System;
using System.Collections.Generic;

namespace Hsec.Application.General.UsuarioRol.Queries.SearchUsuario
{
    public class SearchUsuarioContratistaDto : IMapFrom<TUsuario>
    {
        public int CodUsuario { get; set; }

        public string Usuario { get; set; }

        public string UsuCreacion { get; set; }

        public bool Editable { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SearchUsuarioContratistaDto, TUsuario>();
            //profile.CreateMap<TUsuario, SearchUsuarioContratistaDto>();
            profile.CreateMap<TUsuario, SearchUsuarioContratistaDto>()
                .ForMember(d => d.UsuCreacion, opt => opt.MapFrom(s => s.CreadoPor));
        }
    }
}
