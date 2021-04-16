using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using System.Collections.Generic;

namespace Hsec.Application.Incidentes.Models
{
    public class DetalleDto : IMapFrom<TIncidente>
    {
        public IList<TestigosInvolucradosDetalleDto> TestigosInvolucrados { get; set; }
        public IList<EquipoInvestigacionDetalleDto> EquipoInvestigacion { get; set; }
        public IList<SecuenciaEventosDetalleDto> SecuenciaEventos { get; set; }
        public string ResumenInfMedico { get; set; }
        public string Conclusiones { get; set; }
        public string Aprendizajes { get; set; }
        public string Oculto { get; set; }

        public DetalleDto()
        {
            TestigosInvolucrados = new List<TestigosInvolucradosDetalleDto>();
            EquipoInvestigacion = new List<EquipoInvestigacionDetalleDto>();
            SecuenciaEventos = new List<SecuenciaEventosDetalleDto>();
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DetalleDto, TIncidente>();
            profile.CreateMap<TIncidente, DetalleDto>();
        }
    }
}