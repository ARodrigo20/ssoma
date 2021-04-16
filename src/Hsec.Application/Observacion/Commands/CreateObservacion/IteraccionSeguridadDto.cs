using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Commands.CreateObservacion
{
    public class IteraccionSeguridadDto : IMapFrom<TObservacionIteraccion>
    {
        public IteraccionSeguridadDto()
        {
            MetodologiaGestionRiesgos = new List<String>();
            ActividadAltoRiesgo = new List<String>();
            ClasificacionObservacion = new List<String>();
            ComportamientoRiesgoCondicion = new List<String>();
        }
        public string CodLiderPersona { get; set; }
        public string CodContratista { get; set; }
        public string EquipoInvolucrado { get; set; }
        public string DetalleComportamiento { get; set; }
        public string AccionesInmediatas { get; set; }
        public string IteraccionSeguridad { get; set; }
        public string OtroActividadRiesgo { get; set; }
        public string OtroComponente { get; set; }
        public string ActividadTareaObs { get; set; }
        public string CodStopWork { get; set; }

        public IList<String> MetodologiaGestionRiesgos { get; set; }
        public IList<String> ActividadAltoRiesgo { get; set; }
        public IList<String> ClasificacionObservacion { get; set; }
        public IList<String> ComportamientoRiesgoCondicion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IteraccionSeguridadDto, TObservacionIteraccion>();
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }

    }
}
