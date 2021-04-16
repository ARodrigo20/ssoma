using System.Collections.Generic;
using Hsec.Application.General.PlanAnualGeneral.Models;

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetPlanAnualSeguimientoTotal
{
    public class GetPlanAnualSeguimientoTotalVM
    {
        public ICollection<ModuloEjeDto> ListModulo { get; set; }
        public GetPlanAnualSeguimientoTotalVM()
        {
            this.ListModulo = new HashSet<ModuloEjeDto>();
        }
    }
}