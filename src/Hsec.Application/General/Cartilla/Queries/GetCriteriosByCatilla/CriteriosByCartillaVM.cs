using System.Collections.Generic;

namespace Hsec.Application.General.Cartilla.Queries.GetCriteriosByCatilla
{
    public class CriteriosByCartillaVM
    {
        public ICollection<CriterioDto> list { get; set; }
        public int size { get; set; }
    }
}