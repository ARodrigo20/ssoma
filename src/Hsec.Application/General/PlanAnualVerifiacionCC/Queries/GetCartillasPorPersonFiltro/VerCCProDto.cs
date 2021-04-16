using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;
namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPersonFiltro
{
    public class VerCCProDto : IMapFrom<TPlanAnualVerConCri>
    {
        public int Orden { get; set; }
        public string CodReferencia { get; set; }
        public int NumeroPlanes { get; set; }
        public string Persona { get;set; }
        public string Anio { get; set; }
        public string CodPersona { get; set; }
        public string Gerencia { get; set; }
        public string CodGerencia { get; set; }
        public string CodSuperintendencia { get; set; }
        public string Superintendencia { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TPlanAnualVerConCri,VerCCProDto>();
        }
    }
}