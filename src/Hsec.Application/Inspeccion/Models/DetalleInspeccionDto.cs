using Hsec.Application.Common.Mappings;
using AutoMapper;
using Hsec.Domain.Entities.Inspecciones;
using System.Collections.Generic;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.Inspeccion.Models
{
    public class DetalleInspeccionDto : IMapFrom<TDetalleInspeccion>
    {
        public long? Correlativo { get; set; }
        public string CodInspeccion { get; set; }
        public int? NroDetInspeccion { get; set; }
        public string CodTabla { get; set; }
        public string Lugar { get; set; }
        public string CodUbicacion { get; set; }
        public string DesUbicacion { get; set; }
        public string CodAspectoObs { get; set; }

        public string CodActividadRel { get; set; }
        public string CodNivelRiesgo { get; set; }
        public string Observacion { get; set; }
        // public string Estado { get; set; }
        public bool Estado { get; set; }
        public List<PlanVM> PlanesAccion { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DetalleInspeccionDto, TDetalleInspeccion>();
            profile.CreateMap<TDetalleInspeccion, DetalleInspeccionDto>();
        }
    }
}