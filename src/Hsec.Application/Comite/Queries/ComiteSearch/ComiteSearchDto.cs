using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.Otros;
using System;
using System.Collections.Generic;

namespace Hsec.Application.Comite.Queries.ComiteSearch
{
    public class ComiteSearchDto : IMapFrom<TComite>
    {
        public string CodComite { get; set; }

        public DateTime Fecha { get; set; }

        public string CodTipoComite { get; set; }

        public string CodCategoria { get; set; }

        public string CodPosicionGer { get; set; }

        public string CodPosicionSup { get; set; }

        public string Lugar { get; set; }

        public DateTime FechaCierre { get; set; }

        public string UsuCreacion { get; set; }

        public bool Editable { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ComiteSearchDto, TComite>();
            profile.CreateMap<TComite, ComiteSearchDto>();
            profile.CreateMap<TComite, ComiteSearchDto>()
                .ForMember(d => d.UsuCreacion, opt => opt.MapFrom(s => s.CreadoPor));
        }
    }
}
