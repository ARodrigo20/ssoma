using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;
namespace Hsec.Application.Observacion.Queries.GetResueltosVerCC
{
    public class CartillaDto : IMapFrom<CartillasProVMDto>
    {
        public string CodCartilla { get; set; } 
        public string TipoCartilla { get; set; } 
        public string DescTipoCartilla { get; set; } 
        public string PeligroFatal { get; set; } 
        public string DescPeligroFatal { get; set; } 
        public int Pentidentes { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CartillaDto, CartillasProVMDto>()
            .ForMember(t => t.CodReferencia , opt => opt.MapFrom(t => t.CodCartilla))
            .ForMember(t => t.Valor, opt => opt.MapFrom(t => t.Pentidentes));
        }
    }
}