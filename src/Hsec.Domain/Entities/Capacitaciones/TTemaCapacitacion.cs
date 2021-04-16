using Hsec.Domain.Common;
using System;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TTemaCapacitacion : AuditableEntity
    {
        public TTemaCapacitacion() {
            PlanTema = new HashSet<TPlanTema>();
            TemaCapEspecifico = new HashSet<TTemaCapEspecifico>();
        }
        public string CodTemaCapacita { get; set; } // codcurso
        public string CodTipoTema { get; set; } // codTipo
        public string CodAreaCapacita { get; set; } // codAreaHsec
        public string Descripcion { get; set; }
        public string CompetenciaHs { get; set; } 
        public string CodHha { get; set; } //codHH
        public ICollection<TPlanTema> PlanTema { get; set; }
        public ICollection<TTemaCapEspecifico> TemaCapEspecifico { get; set; }
    }
}