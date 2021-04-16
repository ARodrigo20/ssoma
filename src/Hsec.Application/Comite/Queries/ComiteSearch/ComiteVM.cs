using System;
using System.Collections.Generic;
using System.Text;
using Hsec.Application.Reunion.Models;

namespace Hsec.Application.Comite.Queries.ComiteSearch
{
    public class ComiteVM
    {
        public HashSet<ComiteSearchDto> Lists { get; set; }
        public int count { get; set; }
    }
}
