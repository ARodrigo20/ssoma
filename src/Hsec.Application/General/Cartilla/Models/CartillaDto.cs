using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System.Collections.Generic;

namespace Hsec.Application.General.Cartilla.Models
{
    public class CartillaDto : IMapFrom<TCartilla>
    {
        public CartillaDto()
        {
            Detalle = new HashSet<CartillaDetalleDto>();
        }
        public string CodCartilla { get; set; }
        public string DesCartilla { get; set; }
        public string PeligroFatal { get; set; }
        public string TipoCartilla { get; set; }
        public string Modulo { get; set; }
        public ICollection<CartillaDetalleDto> Detalle { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TCartilla, CartillaDto>();
            profile.CreateMap<CartillaDto, TCartilla>();
        }
    }
}