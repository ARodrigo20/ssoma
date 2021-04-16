using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs
{
    public class GetCursoProgramacionFiltroGenDto
    {
        public GetCursoProgramacionFiltroGenDto()
        {
            expositores = new List<GetCursoProgramacionFiltroGenExpositorDto>();
            participantes = new List<GetCursoProgramacionParticipanteDto>();
        }       
        public string codigo { get; set; }
        public string curso { get; set; }
        public DateTime? fecha { get; set; }
        public string instructor { get; set; }     
        public IList<GetCursoProgramacionFiltroGenExpositorDto> expositores { get; set; }
        public IList<GetCursoProgramacionParticipanteDto> participantes { get; set; }      
    }
}