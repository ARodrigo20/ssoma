using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class GetPlanAccionFiltradoVM
    {
        public GetPlanAccionFiltradoVM()
        {
            data = new List<GetPlanAccionFiltradoDto>();
        }
        public IList<GetPlanAccionFiltradoDto> data { get; set; }
        public int count { get; set; }



    }
}
