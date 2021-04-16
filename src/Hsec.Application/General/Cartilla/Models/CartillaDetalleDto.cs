using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
namespace Hsec.Application.General.Cartilla.Models
{
    public class CartillaDetalleDto : IMapFrom<TCartillaDetalle>
    {
        public string CodCartillaDet { get; set; }
        public string CodCartilla { get; set; }
        public string CodCC { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CartillaDetalleDto, TCartillaDetalle>();
            profile.CreateMap<TCartillaDetalle, CartillaDetalleDto>();
        }
    }
}