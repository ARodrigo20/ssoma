using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using System;

namespace Hsec.Application.Incidentes.Models
{
    public class SecuenciaEventosDetalleDto : IMapFrom<TSecuenciaEvento>
    {
        public string Orden { get; set; }
        public string Evento { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SecuenciaEventosDetalleDto, TSecuenciaEvento>()
                .ForMember(m => m.Correlativo, opt => opt.MapFrom(f => f.Orden));
            profile.CreateMap<SecuenciaEventosDetalleDto, TSecuenciaEvento>();
            profile.CreateMap<TSecuenciaEvento, SecuenciaEventosDetalleDto>();
        }
    }
}