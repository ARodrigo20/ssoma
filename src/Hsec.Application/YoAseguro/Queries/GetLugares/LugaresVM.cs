using System;
using System.Collections.Generic;
using System.Text;
using Hsec.Application.YoAseguro.Models;

namespace Hsec.Application.YoAseguro.Queries.GetLugares
{
    public class LugaresVM
    {
        public ICollection<YoAseguroLugarDto> data { get; set; }
        public int size { get; set; }
    }
}