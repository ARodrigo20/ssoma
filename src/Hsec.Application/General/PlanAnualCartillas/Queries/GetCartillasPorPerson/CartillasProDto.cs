using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;
namespace Hsec.Application.General.PlanAnualCartillas.Queries.GetCartillasPorPerson
{
    public class CartillasProDto : IMapFrom<TPlanAnual>
    {
        public string CodReferencia { get; set; }
        public string Valor { get; set; }
        public string CodMes { get;set; }
        public string Anio { get; set; }
        public string CodPersona { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TPlanAnual,CartillasProDto>();
        }
    }
}