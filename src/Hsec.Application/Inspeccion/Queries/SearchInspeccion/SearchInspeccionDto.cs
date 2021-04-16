using System;
using Hsec.Domain.Entities.Inspecciones;
using AutoMapper;
using Hsec.Application.Common.Mappings;

namespace Hsec.Application.Inspeccion.Queries.SearchInspeccion
{
    public class SearchInspeccionDto : IMapFrom<TInspeccion>
    {
        public string CodInspeccion { get; set; }
        public string CodTabla { get; set; }
        public string CodTipo { get; set; }  //Equipo Inspeccion
        public string CodContrata { get; set; }
        public string DescripcionContrata { get; set; }
        public string AreaAlcance { get; set; }
        public DateTime? FechaP { get; set; }
        public DateTime? Fecha { get; set; }
        public string Hora { get; set; }
        public string Gerencia { get; set; }
        public string DescripcionGer { get; set; }
        public string SuperInt { get; set; }
        public string DescripcionSuperint { get; set; }
        public string CodUbicacion { get; set; }
        public string DescripcionUbicacion { get; set; }
        public string CodSubUbicacion { get; set; }
        public string DescripcionSubUbicacion { get; set; }
        public string Objetivo { get; set; }
        public string Conclusion { get; set; }
        public string Dispositivo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuModifica { get; set; }
        public bool Editable { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TInspeccion, SearchInspeccionDto>()
                .ForMember(d => d.FechaCreacion, opt => opt.MapFrom(s => s.Creado))
                .ForMember(d => d.UsuCreacion, opt => opt.MapFrom(s => s.CreadoPor))
                .ForMember(d => d.FechaModificacion, opt => opt.MapFrom(s => s.Modificado))
                .ForMember(d => d.UsuModifica, opt => opt.MapFrom(s => s.ModificadoPor));
        }
    }
}