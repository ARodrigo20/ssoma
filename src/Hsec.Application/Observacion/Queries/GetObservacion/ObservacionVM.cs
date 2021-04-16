using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Queries.GetObservacion
{
    public class ObservacionVM : IMapFrom<TObservacion> 
    {
        public string CodObservacion { get; set; }
        public string CodPosicionGer { get; set; }
        public string CodPosicionSup { get; set; }
        public string CodAreaHsec { get; set; }
        public string CodTipoObservacion { get; set; }
        public string CodSubTipoObs { get; set; }
        public string CodNivelRiesgo { get; set; }
        public string CodObservadoPor { get; set; }
        public DateTime? FechaObservacion { get; set; }
        public string HoraObservacion { get; set; }
        public string CodUbicacion { get; set; } //?? 
        public string CodSubUbicacion { get; set; } //??
        public string CodUbicacionEspecifica { get; set; } //??
        public string DesUbicacion { get; set; }

        public TareaDto? Tarea { get; set; }
        public IteraccionSeguridadDto? IteraccionSeguridad { get; set; }
        public ComportamientoDto? Comportamiento { get; set; }
        public CondicionDto? Condicion { get; set; }
        public VerificacionControlCriticoDto? VerificacionControlCritico { get; set; }
        public VerificacionControlCriticoDto? Covid19 { get; set; }

        //public IList<PlanAccionVM> PlanAccion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TObservacion,ObservacionVM>();
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }
    }
}
