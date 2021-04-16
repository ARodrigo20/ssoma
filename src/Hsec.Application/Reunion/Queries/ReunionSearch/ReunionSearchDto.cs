using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Application.Reunion.Queries.ReunionGetByCod;
using Hsec.Domain.Entities.Otros;
using System;
using System.Collections.Generic;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Reunion.Queries.ReunionSearch
{
    public class ReunionSearchDto : IMapFrom<TReunion>
    {
        public string CodReunion { get; set; }

        public string Reunion { get; set; }

        public string Lugar { get; set; }

        public DateTime Fecha { get; set; }

        //public PersonaDto CodPerFacilitador { get; set; }
        public PersonaVM CodPerFacilitador { get; set; }

        public string Acuerdos { get; set; }

        public string Comentarios { get; set; }

        public string Otros { get; set; }

        public string UsuCreacion { get; set; }

        public bool Editable { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReunionSearchDto, TReunion>();
            profile.CreateMap<TReunion, ReunionSearchDto>();
            profile.CreateMap<TReunion, ReunionSearchDto>()
                .ForMember(d => d.UsuCreacion, opt => opt.MapFrom(s => s.CreadoPor));
        }
    }
}
