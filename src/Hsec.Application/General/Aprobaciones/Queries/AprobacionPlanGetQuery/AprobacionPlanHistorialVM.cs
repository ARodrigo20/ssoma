using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System;

namespace Hsec.Application.General.Aprobaciones.Queries.AprobacionPlanGetQuery
{
    public class AprobacionPlanHistorialVM : IMapFrom<TAprobacionPlanHistorial>
    {
        public int Correlativo { get; set; }
        public int CodAprobacion { get; set; }
        public string CodPersona { get; set; }
        public string Nombres { get; set; }
        public string Comentario { get; set; }
        public string EstadoAprobacion { get; set; }
        public DateTime Creado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAprobacionPlanHistorial, AprobacionPlanHistorialVM>();
        }
    }
}