using System.Collections.Generic;

namespace Hsec.Application.General.ControlCritico.Queries.SearchControlCritico
{
    public class SearchControlCriticoVM
    {
        public ICollection<SearchControlCriticoDto> list { get; set; }
        public int size { get; set; }
    }
}