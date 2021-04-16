using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create.VMs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create.DTOs
{
    public class CreateCursoProgramacionDto
    {
        public CreateCursoProgramacionDto() {
            expositores = new List<CreateCursoProgramacionExpositorDto>();
            participantes = new List<CreateCursoProgramacionParticipanteDto>();
        }
        public string codTemaCapacita { get; set; } // codcurso
        public string codTipoTema { get; set; } // codTipo
        public string codAreaCapacita { get; set; } // codAreaHsec
        //public string codHha { get; set; } //codHH
        public string codEmpCapacita { get; set; }  // codEmpCap 
        public decimal? puntajeTotal { get; set; } // puntaje total
        public int porcAprobacion { get; set; } // aprobacion
        public string recurrenceID { get; set; } //codigo
        public string codLugarCapacita { get; set; } // codlugar
        public string codSala { get; set; } // codSala
        public int? capacidad { get; set; } //capacidad
        public int? vigencia { get; set; } // vigencia
        public string codVigenciaCapacita { get; set; } // codVigenciaCapacita
        public bool online { get; set; } // nuevo campo Online añadido
        public string duracion { get; set; } // nuevo campo duracion añadido
        public string enlace { get; set; } // nuevo campo enlace añadido
        public IList<CreateCursoProgramacionExpositorDto> expositores { get; set; }
        public IList<CreateCursoProgramacionParticipanteDto> participantes { get; set; }       
        public string fechaInicio { get; set; } // fecInicio
        public string fechaFin { get; set; } // fecFin
    }
}
