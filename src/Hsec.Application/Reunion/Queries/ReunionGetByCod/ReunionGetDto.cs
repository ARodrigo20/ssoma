using Hsec.Application.Reunion.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Reunion.Queries.ReunionGetByCod
{
    public class ReunionGetDto
    {
        public string CodReunion { get; set; }

        public string Reunion { get; set; }

        public string Lugar { get; set; }

        public DateTime Fecha { get; set; }

        public string Hora { get; set; }

        public string CodPerFacilitador { get; set; }

        public string Acuerdos { get; set; }

        public string Comentarios { get; set; }

        public string Otros { get; set; }

        public ICollection<PersonaVM> ReunionAsistentes { get; set; }

        public ICollection<PersonaVM> ReunionAusentes { get; set; }

        public ICollection<PersonaVM> ReunionJustificados { get; set; }

        public ICollection<TAgendaDto> ReunionAgendas { get; set; }

        public ReunionGetDto()
        {
            ReunionAsistentes = new HashSet<PersonaVM>();
            ReunionAusentes = new HashSet<PersonaVM>();
            ReunionJustificados = new HashSet<PersonaVM>();
            ReunionAgendas = new HashSet<TAgendaDto>();
        }
    }
}
