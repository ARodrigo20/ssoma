using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursosFuturos.Queries.DTOs
{
    public class GetCursosFuturosDTO : IMapFrom<TCurso>
    {
        public GetCursosFuturosDTO()
        {
            expositores = new List<GetCursoProgramacionExpositorDto>();
            participantes = new List<GetCursoProgramacionParticipanteDto>();
        }
        public string codTemaCapacita { get; set; } // codcurso
        public string codTipoTema { get; set; } // codTipo
        public string codAreaCapacita { get; set; } // codAreaHsec
        public string codHha { get; set; } //codHH
        public string codEmpCapacita { get; set; }  // codEmpCap 
        public decimal? puntajeTotal { get; set; } // puntaje total
        public int porcAprobacion { get; set; } // aprobacion
        public string codCurso { get; set; } //codigo
        public string codLugarCapacita { get; set; } // codlugar
        public string codSala { get; set; } // codSala
        public int? capacidad { get; set; } //capacidad
        public int? vigencia { get; set; } // vigencia
        public string codVigenciaCapacita { get; set; } // codVigenciaCapacita
        public IList<GetCursoProgramacionExpositorDto> expositores { get; set; }
        public IList<GetCursoProgramacionParticipanteDto> participantes { get; set; }
        public DateTime? fechaInicio { get; set; } // fecInicio
        public DateTime? fechaFin { get; set; } // fecFin

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TCurso, GetCursosFuturosDTO>()
            .ForMember(i => i.codTemaCapacita, opt => opt.MapFrom(t => t.CodTemaCapacita));
        }
    }
}