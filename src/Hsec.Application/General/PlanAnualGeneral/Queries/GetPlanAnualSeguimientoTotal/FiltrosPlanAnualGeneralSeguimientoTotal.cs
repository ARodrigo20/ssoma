using System.Collections.Generic;

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetPlanAnualSeguimientoTotal
{
    public class FiltrosPlanAnualGeneralSeguimientoTotal
    {
        public string Anio { get; set; }
        public string Mes { get; set; }
        public int Gerencia { get; set; }
        public string Persona { get; set; }
        public ICollection<string> CodReferencia { get; set; }
    }
}