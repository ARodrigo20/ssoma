using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Inspeccion.Queries.SearchInspeccion
{
    public class SearchInspeccionVM
    {
        public ICollection<SearchInspeccionDto> data { get; set; }
        public int size { get; set; }

    }
}