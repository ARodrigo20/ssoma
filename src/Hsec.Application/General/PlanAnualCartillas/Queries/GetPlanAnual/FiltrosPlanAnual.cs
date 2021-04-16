using System.Collections.Generic;

namespace Hsec.Application.General.PlanAnual.Queries.GetPlanAnual
{
    public class FiltrosPlanAnual
    {
        public int PaginaTamanio { get; set; }
        public int Pagina { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public int Gerencia { get; set; }
        public ICollection<string> CodReferencia { get; set; }
    }
}