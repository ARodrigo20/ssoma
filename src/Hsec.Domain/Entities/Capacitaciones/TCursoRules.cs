using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TCursoRules : AuditableEntity
    {
        public TCursoRules() {
            TCurso = new HashSet<TCurso>();          
        }
        public string RecurrenceID { get; set; }   
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public bool TipoRecurrenceRule { get; set; }
        //public string rRule { get; set; }
        //public DateTime FechaInicioRule { get; set; }
        //public DateTime FechaFinRule { get; set; }

        //public string CodCurso { get; set; } //codigo
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
        public DateTime FechaLimite { get; set; }
        //public string DescripcionLugar { get; set; }
        //public string TipoVigencia { get; set; }        
        //public string Repetir { get; set; }         
        public ICollection<TCurso> TCurso { get; set; }
    }
}

