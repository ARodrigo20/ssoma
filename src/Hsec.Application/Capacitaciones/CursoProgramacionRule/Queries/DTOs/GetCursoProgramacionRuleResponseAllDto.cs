using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.DTOs
{
    public class GetCursoProgramacionRuleResponseAllDto : IMapFrom<TCursoRules>
    {
        public string id { get; set; } // codCursp
        public string title { get; set; } //descripcion TEMACAPACITA curso left join mediante el codtema
        public string codTemaCapacita { get; set; } // CODTEMA
        public bool estado { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string duration { get; set; }
        public string recurrenceRule { get; set; }
        public bool tipoRecurrenceRule { get; set; }
        //public string recurrenceId { get; set; } // recurrenceId
        //public string recurrenceException { get; set; }
        public string rrule { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public string textColor { get; set; }
        //public string repetir { get; set; }

        //public string codTipoTema { get; set; } // codTipo
        //public string codAreaCapacita { get; set; } // codAreaHsec
        //public string codHha { get; set; } //codHH
        //public string codEmpCapacita { get; set; }  // codEmpCap 
        //public decimal? puntajeTotal { get; set; } // puntaje total
        //public int porcAprobacion { get; set; } // aprobacion        
        //public string codLugarCapacita { get; set; } // codlugar
        //public string codSala { get; set; } // codSala
        //public int? capacidad { get; set; } //capacidad
        //public int? vigencia { get; set; } // vigencia
        //public string codVigenciaCapacita { get; set; } // codVigenciaCapacita
        //public DateTime fechaInicio { get; set; } // fecInicio
        //public DateTime fechaFin { get; set; } // fecFin
        //public DateTime fechaLimite { get; set; }
        //public string descripcionLugar { get; set; }
        //public string tipoVigencia { get; set; }

        public void Mapping(Profile profile)
        {
            //profile.CreateMap<TCursoRules, GetCursoProgramacionRuleResponseAllDto>();

            profile.CreateMap<TCursoRules, GetCursoProgramacionRuleResponseAllDto>()
            .ForMember(i => i.id, opt => opt.MapFrom(t => t.RecurrenceID))
            .ForMember(i => i.codTemaCapacita, opt => opt.MapFrom(t => t.CodTemaCapacita))
            .ForMember(i => i.estado, opt => opt.MapFrom(t => t.Estado))
            .ForMember(i => i.start, opt => opt.MapFrom(t => t.FechaInicio))
            .ForMember(i => i.end, opt => opt.MapFrom(t => t.FechaFin))
            .ForMember(i => i.tipoRecurrenceRule, opt => opt.MapFrom(t => t.TipoRecurrenceRule))
            .ForMember(i => i.recurrenceRule, opt => opt.MapFrom(t => t.RecurrenceRule));
            //.ForMember(i => i.recurrenceId, opt => opt.MapFrom(t => t.RecurrenceID))
            //.ForMember(i => i.recurrenceException, opt => opt.MapFrom(t => t.RecurrenceException))
            //.ForMember(i => i.rrule, opt => opt.MapFrom(t => t.rRule));

            //.ForMember(i => i.repetir, opt => opt.MapFrom(t => t.Repetir))

            //.ForMember(i => i.codTipoTema, opt => opt.MapFrom(t => t.CodTipoTema))
            //.ForMember(i => i.codAreaCapacita, opt => opt.MapFrom(t => t.CodAreaCapacita))
            //.ForMember(i => i.codHha, opt => opt.MapFrom(t => t.CodHha))
            //.ForMember(i => i.codEmpCapacita, opt => opt.MapFrom(t => t.CodEmpCapacita))
            //.ForMember(i => i.puntajeTotal, opt => opt.MapFrom(t => t.PuntajeTotal))
            //.ForMember(i => i.porcAprobacion, opt => opt.MapFrom(t => t.PorcAprobacion))
            //.ForMember(i => i.codLugarCapacita, opt => opt.MapFrom(t => t.CodLugarCapacita))
            //.ForMember(i => i.codSala, opt => opt.MapFrom(t => t.CodSala))
            //.ForMember(i => i.capacidad, opt => opt.MapFrom(t => t.Capacidad))
            //.ForMember(i => i.vigencia, opt => opt.MapFrom(t => t.Vigencia))
            //.ForMember(i => i.codVigenciaCapacita, opt => opt.MapFrom(t => t.CodVigenciaCapacita))
            //.ForMember(i => i.fechaInicio, opt => opt.MapFrom(t => t.FechaInicio))
            //.ForMember(i => i.fechaFin, opt => opt.MapFrom(t => t.FechaFin))
            //.ForMember(i => i.fechaLimite, opt => opt.MapFrom(t => t.FechaLimite))
            //.ForMember(i => i.descripcionLugar, opt => opt.MapFrom(t => t.DescripcionLugar))
            //.ForMember(i => i.tipoVigencia, opt => opt.MapFrom(t => t.TipoVigencia));
        }
    }
}
