using Hsec.Application.Common.Models;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.VerficacionesCc;
using Hsec.Domain.Enums;
using System.Collections.Generic;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.VerificacionControlCritico.Commands.CreateVerificacionControlCritico
{
    public class VerificacionControlCriticoDto  : IMapFrom<TVerificacionControlCritico>
    {
        public string CodigoVCC { get; set; }
        public string Fecha { get; set; }
        public string Gerencia { get; set; }
        public string SuperIndendecnia { get; set; }
        public string CodResponsable { get; set; }
        public string Empresa {get;set;}
        public string Cartilla {get;set;}

        public IList<CriterioEvaluacionDto> Criterios {get;set;}

        public IList<PlanVM> PlanAccion {get;set;}
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VerificacionControlCriticoDto, TVerificacionControlCritico>();
            profile.CreateMap<TVerificacionControlCritico, VerificacionControlCriticoDto>();
        }
    }
}
