using Hsec.Application.Common.Models;
using Hsec.Application.Reunion.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.Reunion.Command.ReunionCreate
{
    public class ReunionCreateDto
    {
        public string Reunion { get; set; }

        public string Lugar { get; set; }

        public DateTime Fecha { get; set; }

        public string Hora { get; set; }

        public string CodPerFacilitador { get; set; }

        public string Acuerdos { get; set; }

        public string Comentarios { get; set; }

        public string Otros { get; set; }

        public ICollection<string> ReunionAsistentes { get; set; }

        public ICollection<string> ReunionAusentes { get; set; }

        public ICollection<string> ReunionJustificados { get; set; }

        public ICollection<TAgendaDto> ReunionAgendas { get; set; }

        public List<PlanVM> PlanAccion { get; set; }
    }
}
