using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Roles.Queries.GetRolPerfil
{
    public class AccesosDto  : IMapFrom<TAcceso>
    {
        public AccesosDto(){
            this.data = new AccesoData();
        }
        public AccesoData data {get;set;}
        public bool expanded {get;set;}
        public IList<AccesosDto> children { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AccesosDto, TAcceso>()
                .ForMember(t => t.CodAcceso , opt => opt.MapFrom(t => t.data.CodAcceso))
                .ForMember(t => t.Descripcion , opt => opt.MapFrom(t => t.data.Descripcion))
                .ForMember(t => t.Hijos , opt => opt.MapFrom(t => t.children));
            profile.CreateMap<TAcceso, AccesosDto>()
                .ForMember(t => t.data, opt => opt.MapFrom(t => t))
                .ForMember(t => t.expanded, opt => opt.MapFrom(t => t.Hijos!=null && t.Hijos.Count!=0))
                .ForMember(t => t.children, opt => opt.MapFrom(t => t.Hijos));
        }
    }

    public class AccesoData  : IMapFrom<TAcceso>
    {
        public AccesoData()
        {
        }

        public int CodAcceso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; } // texto alternativo al nombre- tooltip
        // public string Componente { get; set; }
        // public string Icono { get; set; }
        // public string BadgeVariant { get; set; }
        // public string BadgeText { get; set; } 

        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AccesoData, TAcceso>();
            profile.CreateMap<TAcceso, AccesoData>();
        }
    }
}