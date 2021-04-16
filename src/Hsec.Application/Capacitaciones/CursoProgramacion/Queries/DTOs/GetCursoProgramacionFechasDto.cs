using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs
{
    public class GetCursoProgramacionFechasDto
    {
        //public string codCurso { get; set; } //codigo
        //public string recurrenceID { get; set; }
        //public string descripcion { get; set; } // descripcion del tema
        //public string codTemaCapacita { get; set; }
        //public DateTime fechaInicio { get; set; } // fecInicio
        //public DateTime fechaFin { get; set; } // fecFin

        public string id { get; set; } //codigo
        //public string recurrenceID { get; set; }
        public string title { get; set; } // descripcion del tema
        public string codTemaCapacita { get; set; }
        public DateTime start { get; set; } // fecInicio
        public DateTime end { get; set; } // fecFin       
        public string duration { get; set; }
        public string recurrenceId { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public string textColor { get; set; }

    }
}
