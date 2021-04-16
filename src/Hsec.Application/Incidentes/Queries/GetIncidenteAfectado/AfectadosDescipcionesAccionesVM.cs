using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using System.Collections.Generic;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectado
{
    public class AfectadosDescipcionesAccionesVM : IMapFrom<TDetalleAfectado>
    {
        public AfectadosDescipcionesAccionesVM()
        {
            Afectados = new List<DetalleAfectadoDto>();
        }
        public IList<DetalleAfectadoDto> Afectados { get; set; }
        
        public string DesSuceso { get; set; }
        public string DesDanioLesImpacPerd { get; set; }
        public string AccInmediatas { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AfectadosDescipcionesAccionesVM, TDetalleAfectado>();
            profile.CreateMap<TDetalleAfectado, AfectadosDescipcionesAccionesVM>();
        }
    }
}