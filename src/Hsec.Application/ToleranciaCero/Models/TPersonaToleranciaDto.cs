using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Otros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.ToleranciaCero.Models
{
    public class TPersonaToleranciaDto : IMapFrom<TPersonaTolerancia>
    {
        public string CodTolCero { get; set; }

        public string CodPersona { get; set; }

        public int Correlativo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TPersonaToleranciaDto, TPersonaTolerancia>();
            profile.CreateMap<TPersonaTolerancia, TPersonaToleranciaDto>();
        }
    }
}
