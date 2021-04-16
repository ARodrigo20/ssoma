using System;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.VerficacionesCc;
using Hsec.Domain.Enums;
namespace Hsec.Application.VerificacionControlCritico.Queries.BuscarVerificacionControlCritico
{
    public class ItemBVCCDto : IMapFrom<TVerificacionControlCritico>
    {
        public string CodigoVCC { get; set; }
        public string Gerencia { get; set; }
        public string CodResponsable { get; set; }
        public float Avance { get; set; }
        public string Cartilla { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public DateTime FechaCreacion { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ItemBVCCDto, TVerificacionControlCritico>()
            .ForMember(t => t.Creado , opt => opt.MapFrom(t => t.FechaCreacion));
            profile.CreateMap<TVerificacionControlCritico, ItemBVCCDto>()
            .ForMember(t => t.FechaCreacion , opt => opt.MapFrom(t => t.Creado));
        }
    }
}