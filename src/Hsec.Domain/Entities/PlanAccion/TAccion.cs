using Hsec.Domain.Common;
using System;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.PlanAccion
{
    public class TAccion : AuditableEntity
    {
        public TAccion()
        {
            RespPlanAccion = new HashSet<TResponsable>();
            LevantamientoPlan = new HashSet<TLevantamientoPlan>();
        }
        public int CodAccion { get; set; }
        public string CodActiRelacionada { get; set; }
        public string CodAreaHsec { get; set; }
        public string CodEstadoAccion { get; set; }
        public string CodNivelRiesgo { get; set; }
        public string CodTipoAccion { get; set; }
        public string? CodSolicitadoPor { get; set; }
        public string? DocReferencia { get; set; }
        public string? DocSubReferencia { get; set; }
        public ICollection<TResponsable> RespPlanAccion { get; set; }
        public ICollection<TLevantamientoPlan> LevantamientoPlan { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string? Tarea { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string CodTablaRef { get; set; }
        public string Aprobador { get; set; }
        public string EstadoAprobacion { get; set; }


    }
}
