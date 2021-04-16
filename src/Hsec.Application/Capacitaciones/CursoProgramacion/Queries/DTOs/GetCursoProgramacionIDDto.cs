using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs
{
    public class GetCursoProgramacionIDDto 
    {
        public GetCursoProgramacionIDDto()
        {
            expositores = new List<GetCursoProgramacionExpositorDto>();
            participantes = new List<GetCursoProgramacionParticipanteDto>();
        }

        public string id { get; set; } // codCursp
        //public string title { get; set; } //descripcion TEMACAPACITA curso left join mediante el codtema
        public string codTemaCapacita { get; set; } // CODTEMA
        public bool estado { get; set; }
        //public DateTime start { get; set; }
        //public DateTime end { get; set; }
        //public string recurrenceRule { get; set; }
        //public bool tipoRecurrenceRule { get; set; }
        public string recurrenceId { get; set; } // recurrenceId
        //public string recurrenceException { get; set; }
        //public string rrule { get; set; }
        //public string repetir { get; set; }

        public string codTipoTema { get; set; } // codTipo
        public string codAreaCapacita { get; set; } // codAreaHsec
       // public string codHha { get; set; } //codHH
        public string codEmpCapacita { get; set; }  // codEmpCap 
        public decimal? puntajeTotal { get; set; } // puntaje total
        public int porcAprobacion { get; set; } // aprobacion        
        public string codLugarCapacita { get; set; } // codlugar
        public string codSala { get; set; } // codSala
        public int? capacidad { get; set; } //capacidad
        public int? vigencia { get; set; } // vigencia
        public string enlace { get; set; }
        public string duracion { get; set; }
        public bool online { get; set; }

        public string codVigenciaCapacita { get; set; } // codVigenciaCapacita
        public DateTime fechaInicio { get; set; } // fecInicio
        public DateTime fechaFin { get; set; } // fecFin
        //public DateTime fechaLimite { get; set; }
        public string descripcionLugar { get; set; }
        public string tipoVigencia { get; set; }
        public IList<GetCursoProgramacionExpositorDto> expositores { get; set; }
        public IList<GetCursoProgramacionParticipanteDto> participantes { get; set; }                 
    }
}
