using System;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class PlanAccionMaestroAllVM
    {
        public int CodAccion { get; set; }
        public int CodActiRelacionada { get; set; }
        public int CodAreaHsec { get; set; }
        public int CodEstadoAccion { get; set; }
        public int CodNivelRiesgo { get; set; }
        public int CodTipoAccion { get; set; }
        public string CodSolicitadoPor { get; set; }
        public string CreadoPor { get; set; }
        public DateTime Creado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }
        public bool Estado { get; set; }
    }
}
