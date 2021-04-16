using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.YoAseguro.Queries.SearchYoAseguro
{
    public class SearchYoAseguroVM
    {
        public ICollection<SearchYoAseguroDto> data { get; set; }
        public int size { get; set; }
    }
}
