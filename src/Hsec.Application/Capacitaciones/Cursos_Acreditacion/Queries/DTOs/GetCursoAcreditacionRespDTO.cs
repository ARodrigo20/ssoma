using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Cursos_Acreditacion.Queries.DTOs
{
    public class GetCursoAcreditacionRespDTO
    {
        //public string codigo { get; set; }
        public string descripcion { get; set; }
        public string codTemaCapacita { get; set; }
        public DateTime? fechaProgramada { get; set; }
        public string nota { get; set; }

        public string codCurso { get; set; }
        public string codPersona { get; set; }
        public string codStiker { get; set; }
        public string candado { get; set; } // CodTarjeta       
        public DateTime? fechaStiker { get; set; }
        public DateTime? fechaTarjeta { get; set; }
        //public string nombreCurso { get; set; }


        //public string curso { get; set; }        
    }
}