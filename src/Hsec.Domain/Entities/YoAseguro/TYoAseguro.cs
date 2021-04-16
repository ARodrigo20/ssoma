using Hsec.Domain.Common;
using System;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.YoAseguro
{
    public class TYoAseguro : AuditableEntity
    {
        public TYoAseguro()
        {
            PersonasReconocidas = new HashSet<TPersonaYoAseguro>();
        }
        public string CodYoAseguro { get; set; }
        public string CodPosGerencia { get; set; }
        public string CodPersonaResponsable { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? FechaEvalucion { get; set; }
        public int ReportadosObservaciones { get; set; }
        public int CorregidosObservaciones { get; set; }
        public string ObsCriticaDia { get; set; }
        public string Calificacion { get; set; }
        public string Comentario { get; set; }
        public string Reunion { get; set; }
        public string Recomendaciones { get; set; }
        public string TituloReunion { get; set; }
        public string TemaReunion { get; set; }
        public ICollection<TPersonaYoAseguro> PersonasReconocidas { get; set; }
        
    }
}
