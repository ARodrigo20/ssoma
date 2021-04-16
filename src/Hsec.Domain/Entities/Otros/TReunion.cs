using System;
using System.Collections.Generic;
using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Otros
{
    public class TReunion : AuditableEntity
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

        public ICollection<TAsistentesReunion> ReunionAsistentes { get; set; }

        public ICollection<TAusentesReunion> ReunionAusentes { get; set; }

        public ICollection<TJustificadosReunion> ReunionJustificados { get; set; }

        public ICollection<TAgenda> ReunionAgendas { get; set; }

        public TReunion()
        {
            ReunionAsistentes = new HashSet<TAsistentesReunion>();
            ReunionAusentes = new HashSet<TAusentesReunion>();
            ReunionJustificados = new HashSet<TJustificadosReunion>();
            ReunionAgendas = new HashSet<TAgenda>();
        }
    }
}
