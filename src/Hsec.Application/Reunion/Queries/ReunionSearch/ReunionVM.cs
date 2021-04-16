using System;
using System.Collections.Generic;
using System.Text;
using Hsec.Application.Reunion.Models;

namespace Hsec.Application.Reunion.Queries.ReunionSearch
{
    public class ReunionVM
    {
        public HashSet<ReunionSearchDto> Lists { get; set; }
        public int count { get; set; }
    }
}
