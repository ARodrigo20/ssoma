using System.Collections.Generic;

namespace Hsec.Application.General.Cartilla.Queries.SearchCartilla
{
    public class SearchCartillaVM
    {
        public ICollection<SearchCartillaDto> list { get; set; }
        public int size { get; set; }
    }
}