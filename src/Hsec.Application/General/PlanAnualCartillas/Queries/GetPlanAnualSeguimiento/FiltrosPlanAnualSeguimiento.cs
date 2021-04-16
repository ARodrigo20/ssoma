using System.Collections.Generic;

namespace Hsec.Application.General.PlanAnualCartillas.Queries.GetPlanAnualSeguimiento
{
    public class FiltrosPlanAnualSeguimiento
    {
        public int PaginaTamanio { get; set; }
        public int Pagina { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public int Gerencia { get; set; }
        public string Persona { get; set; }
        public ICollection<string> CodReferencia { get; set; }
    }
}