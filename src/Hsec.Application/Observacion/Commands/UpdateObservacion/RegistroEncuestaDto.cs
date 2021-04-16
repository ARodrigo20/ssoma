using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion
{
    public class RegistroEncuestaDto : IMapFrom<TObsTaRegistroEncuesta>
    {
        public string CodRespuesta { get; set; }
        public string CodPregunta { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegistroEncuestaDto,TObsTaRegistroEncuesta>();
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }
    }
}
