using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TCurso : AuditableEntity
    {
        public TCurso() {
            Expositores = new HashSet<TExpositor>();
            Participantes = new HashSet<TParticipantes>();
            Preguntas = new HashSet<TPreguntas>();
        }

        public string CodCurso { get; set; } //codigo
        public string RecurrenceID { get; set; }
        public string CodTemaCapacita { get; set; } // codcurso
        public string CodTipoTema { get; set; } // codTipo
        public string CodAreaCapacita { get; set; } // codAreaHsec
        //public string CodHha { get; set; } //codHH
        public string CodEmpCapacita { get; set; }  // codEmpCap 
        public decimal? PuntajeTotal { get; set; } // puntaje total
        public int PorcAprobacion { get; set; } // aprobacion        
        public string CodLugarCapacita { get; set; } // codlugar
        public string CodSala { get; set; } // codSala
        public int? Capacidad { get; set; } //capacidad
        public int? Vigencia { get; set; } // vigencia
        public string CodVigenciaCapacita { get; set; } // codVigenciaCapacita
        public DateTime FechaInicio { get; set; } // fecInicio
        public DateTime FechaFin { get; set; } // fecFin
        //public string DescripcionLugar { get; set; }        
        //public string TipoVigencia { get; set; }        
        public bool Online { get; set; } // Online true= sincrono, False = asincrono, Cursos con duracion independiente a la fecha inicio y fin
        public string Enlace { get; set; }
        public string Duracion { get; set; }
        public ICollection<TExpositor> Expositores { get; set; }
        public ICollection<TParticipantes> Participantes { get; set; }
        public ICollection<TPreguntas> Preguntas { get; set; }
        public TCursoRules TCursoRules { get; set; }
    }
}
