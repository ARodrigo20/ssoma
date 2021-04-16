using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Queries.GetPersonasJerarquia
{
    public class GetAccionPersonaJerarquiaVM
    {
        public GetAccionPersonaJerarquiaVM()
        {
            data = new List<GetAccionPersonaJerarquiaDto>();
        }
        public IList<GetAccionPersonaJerarquiaDto> data { get; set; }
        public int count { get; set; }
    }
}
