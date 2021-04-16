using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Queries.GetLevatamientoTareas
{
    public class GetLevTareasVM
    {
        public GetLevTareasVM()
        {
            data = new List<GetLevTareasDto>();
        }
        public IList<GetLevTareasDto> data { get; set; }
        public int count { get; set; }
    }
}
