using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.PlanAccion.Queries.GetPlanDeAccion;
using Hsec.Domain.Entities.PlanAccion;
using System;
using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion
{
    public class PlanVM : IMapFrom<TAccion>
    {
        public PlanVM()
        {
            RespPlanAccion = new List<ResponsablesDto>();
        }
        //codAccion: number;

        public int codAccion { get; set; }
        public string codAreaHsec { get; set; }
        public string codTipoAccion { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public string codActiRelacionada { get; set; }
        public string codSolicitadoPor { get; set; }
        public string nombreSolicitadoPor { get; set; }
        public string docReferencia { get; set; }
        public string docSubReferencia { get; set; }
        public string codNivelRiesgo { get; set; }
        public string tarea { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }
        public bool estado { get; set; }
        public string codTablaRef { get; set; }
        public string Aprobador { get; set; }
        public string EstadoAprobacion { get; set; }
        public string codEstadoAccion { get; set; }
        //public string estadoPlan { get; set; }
        public IList<ResponsablesDto> RespPlanAccion { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAccion, PlanVM>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));    
        }
    }
}
