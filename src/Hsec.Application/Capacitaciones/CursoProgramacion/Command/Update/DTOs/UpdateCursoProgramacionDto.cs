using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Command.Update.DTOs
{
    public class UpdateCursoProgramacionDto : IMapFrom<TCurso>
    {
        public UpdateCursoProgramacionDto() {
            expositores = new List<UpdateCursoProgramacionExpositorDto>();
            participantes = new List<UpdateCursoProgramacionParticipanteDto>();
        }
        public string codTemaCapacita { get; set; } // codcurso
        public string codTipoTema { get; set; } // codTipo
        public string codAreaCapacita { get; set; } // codAreaHsec
        //public string codHha { get; set; } //codHH
        public string codEmpCapacita { get; set; }  // codEmpCap 
        public decimal? puntajeTotal { get; set; } // puntaje total
        public int porcAprobacion { get; set; } // aprobacion
        public string codCurso { get; set; } //codigo
        public string codLugarCapacita { get; set; } // codlugar
        public string codSala { get; set; } // codSala
        public int? capacidad { get; set; } //capacidad
        public int? vigencia { get; set; } // vigencia
        public string codVigenciaCapacita { get; set; } // codVigenciaCapacita
        public bool online { get; set; } // nuevo campo Online añadido
        public string duracion { get; set; } // nuevo campo duracion añadido
        public string enlace { get; set; } // nuevo campo enlace añadido
        public IList<UpdateCursoProgramacionExpositorDto> expositores { get; set; }
        public IList<UpdateCursoProgramacionParticipanteDto> participantes { get; set; }
        public DateTime fechaInicio { get; set; } // fecInicio
        public DateTime fechaFin { get; set; } // fecFin

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TCurso, UpdateCursoProgramacionDto>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));    
        }
    }
}
