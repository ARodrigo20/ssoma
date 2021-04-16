using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.DTOs;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.VMs
{
    public class GetCursoProgramacionRuleFiltroRequestVM : IMapFrom<TCursoRules>
    {
        public string id { get; set; }
        public string recurrenceId { get; set; }
        public string recurrenceRule { get; set; }
        public string recurrenceException { get; set; }
        public bool tipoRecurrenceRule { get; set; }      
        public DateTime fechaLimite { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TCursoRules, GetCursoProgramacionRuleFiltroRequestVM>()
                 //.ForMember(i => i.id, opt => opt.MapFrom(t => t.CodCurso))
                 .ForMember(i => i.start, opt => opt.MapFrom(t => t.FechaInicio))
                 .ForMember(i => i.end, opt => opt.MapFrom(t => t.FechaFin));
        }

        //public void Mapping(Profile profile)
        //{
        //    //profile.CreateMap<TCursoRules, GetCursoProgramacionRuleResponseAllDto>();
        //    profile.CreateMap<TCursoRules, CreateCursoProgramacionRuleDto>()
        //    .ForMember(i => i.id, opt => opt.MapFrom(t => t.CodCurso))
        //    .ForMember(i => i.codTema, opt => opt.MapFrom(t => t.CodTemaCapacita))
        //    .ForMember(i => i.start, opt => opt.MapFrom(t => t.FechaInicioRule))
        //    .ForMember(i => i.end, opt => opt.MapFrom(t => t.FechaFinRule))
        //    .ForMember(i => i.tipoRecurrenceRule, opt => opt.MapFrom(t => t.TipoRecurrenceRule))           
        //    .ForMember(i => i.recurrenceRule, opt => opt.MapFrom(t => t.RecurrenceRule))
        //    .ForMember(i => i.recurrenceException, opt => opt.MapFrom(t => t.RecurrenceException))
        //    ;
        //}
    }
}
