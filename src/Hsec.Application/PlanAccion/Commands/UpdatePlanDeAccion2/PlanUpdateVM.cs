using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.PlanAccion;
using System;
using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion2
{
    public class PlanUpdateVM : IMapFrom<TAccion>
    {
        public PlanUpdateVM()
        {
            RespPlanAccion = new List<ResposablesDtoUpdate>();
        }
        //codAccion: number;

        public int codAccion { get; set; }
        public string codAreaHsec { get; set; }
        public string codTipoAccion { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public string codActiRelacionada { get; set; }
        public string codSolicitadoPor { get; set; }
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
        //public string estadoPlan { get; set; }
        public IList<ResposablesDtoUpdate> RespPlanAccion { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAccion, PlanUpdateVM>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));    
        }
    }
}
