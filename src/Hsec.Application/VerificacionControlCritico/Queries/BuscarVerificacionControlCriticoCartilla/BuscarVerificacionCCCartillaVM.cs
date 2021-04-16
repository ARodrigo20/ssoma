using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
//using Hsec.Application.Common.Models;
using Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPersonFiltro;
using Hsec.Domain.Entities.VerficacionesCc;
using Hsec.Domain.Enums;
namespace Hsec.Application.VerificacionControlCritico.Queries.BuscarVerificacionControlCriticoCartilla
{
    public class BuscarVerificacionCCCartillaVM  : IMapFrom<VerCCPorPersonaFiltroVM>
    {
        
        public IList<ItemBVCCDto> List {get;set;}

        public int Pagina { get; set; }
        public int Count { get; set; }
        public void Mapping(Profile profile)
        {
            // profile.CreateMap<ItemBVCCDto, VerCCPorPersonaFiltroVM>();
            
            profile.CreateMap<VerCCPorPersonaFiltroVM, BuscarVerificacionCCCartillaVM>()
            .ForMember(t => t.Pagina , opt => opt.MapFrom(t => t.Pagina))
            .ForMember(t => t.Count , opt => opt.MapFrom(t => t.Count))
            .ForMember(t => t.List , opt => opt.MapFrom(t => t.list));
        }
    }
}