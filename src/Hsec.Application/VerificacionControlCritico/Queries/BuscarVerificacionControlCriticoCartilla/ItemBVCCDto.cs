using System;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPersonFiltro;


namespace Hsec.Application.VerificacionControlCritico.Queries.BuscarVerificacionControlCriticoCartilla
{
    public class ItemBVCCDto : IMapFrom<VerCCProDto>
    {
        public int Orden { get; set; }
        public string Cartilla { get; set; }
        public string Anio { get; set; }
        public string CodResponsable { get; set; }
        public string Responsable { get; set; }
        public string Gerencia { get; set; }
        public string CodGerencia { get; set; }
        public string SuperIndendecnia { get; set; }
        public string CodSuperIndendecnia { get; set; }
        public int Avance { get; set; }
        // public string Mes { get; set; }
        // public DateTime FechaCreacion { get; set; }
        public void Mapping(Profile profile)
        {
            // profile.CreateMap<ItemBVCCDto, VerCCProFiltroVM>();
            
            //profile.CreateMap<VerCCProFiltroVM, ItemBVCCDto>()
            profile.CreateMap<VerCCProDto, ItemBVCCDto>()
            .ForMember(t => t.Cartilla , opt => opt.MapFrom(t => t.CodReferencia))
            .ForMember(t => t.CodResponsable , opt => opt.MapFrom(t => t.CodPersona))
            .ForMember(t => t.Responsable , opt => opt.MapFrom(t => t.Persona))
            .ForMember(t => t.Gerencia , opt => opt.MapFrom(t => t.Gerencia))
            .ForMember(t => t.CodGerencia , opt => opt.MapFrom(t => t.CodGerencia))
            .ForMember(t => t.SuperIndendecnia , opt => opt.MapFrom(t => t.Superintendencia))
            .ForMember(t => t.CodSuperIndendecnia , opt => opt.MapFrom(t => t.CodSuperintendencia))
            .ForMember(t => t.Avance , opt => opt.MapFrom(t => t.NumeroPlanes));
        }
    }
}