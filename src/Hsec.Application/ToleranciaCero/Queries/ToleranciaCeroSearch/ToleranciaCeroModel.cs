using Hsec.Application.ToleranciaCero.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.ToleranciaCero.Queries.ToleranciaCeroSearch
{
    public class ToleranciaCeroModel
    {
        public HashSet<ToleranciaCeroDto> Lists { get; set; }
        public int count { get; set; }

    }
}
