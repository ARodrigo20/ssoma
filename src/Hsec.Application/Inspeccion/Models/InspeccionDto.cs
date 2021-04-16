using System;
using Hsec.Application.Common.Mappings;
using AutoMapper;
using Hsec.Domain.Entities.Inspecciones;

namespace Hsec.Application.Inspeccion.Models
{
    public class InspeccionDto : IMapFrom<TInspeccion>
    {
        public string CodInspeccion { get; set; }
        public string CodTabla { get; set; }
        public string CodTipo { get; set; }  //Equipo Inspeccion
        public string CodContrata { get; set; }
        public string AreaAlcance { get; set; }
        public DateTime? FechaP { get; set; }
        public DateTime? Fecha { get; set; }
        public string Hora { get; set; }
        public string Gerencia { get; set; }
        public string SuperInt { get; set; }
        public string CodUbicacion { get; set; }
        public string CodSubUbicacion { get; set; }
        public string Objetivo { get; set; }
        public string Conclusion { get; set; }
        public string Dispositivo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InspeccionDto, TInspeccion>();
            profile.CreateMap<TInspeccion, InspeccionDto>();
        }
    }
}