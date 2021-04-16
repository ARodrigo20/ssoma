using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Verficaciones;
using System;

namespace Hsec.Application.Verificaciones.Queries.GetBuscarVerificacion
{
    public class VerificacionBuscarDto : IMapFrom<TVerificaciones>
    {

        public string CodVerificacion { get; set; }
        public string Tipo { get; set; }
        public string AreaHSEC { get; set; }
        public string NivelRiesgo { get; set; }
        public string VerificadoPor { get; set; }
        public string StopWork { get; set; }
        public DateTime FechaVerificacion { get; set; }
        public string HoraVerificacion { get; set; }
        public string Gerencia { get; set; }
        public string Superintendencia { get; set; }
        public string CodTabla { get; set; }
        public string Ubicacion { get; set; }
        public string SubUbicacion { get; set; }
        public string UbicacionEspecifica { get; set; }
        public string CreadoPor { get; set; }
        public DateTime Creado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }
        public bool Editable { get; set; }
        //---------------------
        // public string CodPosicionGer {get;set;}
        // public string CodPosicionSup {get;set;}
        // public string CodAreaHSEC {get;set;}
        // public string CodTipoVerificacion {get;set;}
        // public string CodNivelRiesgo {get;set;}
        // public string CodVerificacionPor {get;set;}
        // public string CodUbicacion {get;set;}
        // public string CodSubUbicacion {get;set;}
        // public string CodUbicacionEspecifica {get;set;}
        // public string DesUbicacion {get;set;}
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TVerificaciones, VerificacionBuscarDto>()
            .ForMember(t => t.Gerencia , m => m.MapFrom(t => t.CodPosicionGer))
            .ForMember(t => t.Superintendencia , m => m.MapFrom(t => t.CodPosicionSup))
            .ForMember(t => t.AreaHSEC , m => m.MapFrom(t => t.CodAreaHSEC))
            .ForMember(t => t.Tipo , m => m.MapFrom(t => t.CodTipoVerificacion))
            .ForMember(t => t.NivelRiesgo , m => m.MapFrom(t => t.CodNivelRiesgo))
            .ForMember(t => t.VerificadoPor , m => m.MapFrom(t => t.CodVerificacionPor))
            .ForMember(t => t.Ubicacion , m => m.MapFrom(t => t.CodUbicacion))
            .ForMember(t => t.SubUbicacion , m => m.MapFrom(t => t.CodSubUbicacion))
            .ForMember(t => t.UbicacionEspecifica , m => m.MapFrom(t => t.CodUbicacionEspecifica));
        }
    }
}