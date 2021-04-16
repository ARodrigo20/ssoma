using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System.Collections.Generic;

namespace Hsec.Application.General.Aprobaciones.Queries.AprobacionPlanGetQuery
{
    public class AprobacionPlanVM : IMapFrom<TAprobacionPlan>
    {
        public AprobacionPlanVM()
        {
            historial = new List<AprobacionPlanHistorialVM>();
        }
        public int CodAprobacion { get; set; }
        public int CodAccion { get; set; }
        public string DocReferencia { get; set; }
        public string EstadoDoc { get; set; }
        public string CodTabla { get; set; }
        public List<AprobacionPlanHistorialVM> historial { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAprobacionPlan, AprobacionPlanVM>();
        }
    }
}