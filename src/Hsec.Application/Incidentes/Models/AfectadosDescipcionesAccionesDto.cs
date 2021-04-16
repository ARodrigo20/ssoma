using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using System.Collections.Generic;

namespace Hsec.Application.Incidentes.Models
{
    public class AfectadosDescipcionesAccionesDto : IMapFrom<TDetalleAfectado>
    {
        public AfectadosDescipcionesAccionesDto()
        {
            Afectados = new List<DetalleAfectadoDto>();
        }
        public List<DetalleAfectadoDto> Afectados { get; set; }
        
        public string DesSuceso { get; set; }
        public string DesDanioLesImpacPerd { get; set; }
        public string AccInmediatas { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AfectadosDescipcionesAccionesDto, TDetalleAfectado>();
            profile.CreateMap<TDetalleAfectado, AfectadosDescipcionesAccionesDto>();
        }
    }
}