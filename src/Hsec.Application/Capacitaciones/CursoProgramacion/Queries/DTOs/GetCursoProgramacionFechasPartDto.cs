using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs
{
    public class GetCursoProgramacionFechasPartDto
    {
        public GetCursoProgramacionFechasPartDto()
        {
            participantes = new List<GetCursoProgramacionParticipanteDto>();
        }

        public string id { get; set; } //codigo     
        public string title { get; set; } // descripcion del tema
        public string codTemaCapacita { get; set; }
        public DateTime start { get; set; } // fecInicio
        public DateTime end { get; set; } // fecFin
        public string duration { get; set; }
        public string recurrenceID { get; set; }
        public IList<GetCursoProgramacionParticipanteDto> participantes { get; set; }
    }
}
