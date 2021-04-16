using System;
using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;

namespace Hsec.Application.Observacion.Commands.CreateObservacion
{
    public class VerificacionControlCriticoDto : IMapFrom<TObservacionVerControlCritico>
    {
        public string CodObservacion {get;set;}
        public string CodVcc {get;set;}
        public string CodCartilla {get;set;}
        //public DateTime Fecha {get;set;}
        public string CodObservadoPor {get;set;}
        public string CodPosicionGer {get;set;}
        public string TareaObservada {get;set;}
        public string Empresa {get;set;}
        
        public ICollection<string> Herramientas { get; set; }
        public ICollection<CriterioDto> Criterios { get; set; }
        public ICollection<CierreInteraccionDto> CierreInteracion { get; set; }

        public VerificacionControlCriticoDto(){
            Herramientas = new HashSet<string>();
            Criterios = new HashSet<CriterioDto>();
            CierreInteracion = new HashSet<CierreInteraccionDto>();
        }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VerificacionControlCriticoDto, TObservacionVerControlCritico>();
            profile.CreateMap<string, TObsVCCHerramienta>()
            .ForMember(t => t.CodDesHe , opt => opt.MapFrom(t => t));
        }
    }
}

