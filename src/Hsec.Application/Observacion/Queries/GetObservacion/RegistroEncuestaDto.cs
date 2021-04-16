using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Queries.GetObservacion

{
    public class RegistroEncuestaDto : IMapFrom<TObsTaRegistroEncuesta>
    {
        public string CodRespuesta { get; set; }
        public string CodPregunta { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TObsTaRegistroEncuesta,RegistroEncuestaDto>();
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }
    }
}
