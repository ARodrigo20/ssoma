using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Otros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.ToleranciaCero.Models
{
    public class TRegTolDetalleDto : IMapFrom<TRegTolDetalle>
    {
        public string CodRegla { get; set; }

        public string CodTolCero { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TRegTolDetalleDto, TRegTolDetalle>();
            profile.CreateMap<TRegTolDetalle, TRegTolDetalleDto>();
        }
    }
}
