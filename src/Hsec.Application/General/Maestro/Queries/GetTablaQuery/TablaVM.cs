using System.Collections.Generic;
using Hsec.Application.General.Maestro.Models;

namespace Hsec.Application.General.Maestro.Queries.GetTablaQuery
{
    public class TablaVM
    {
        public ICollection<MaestroDto> data { get; set; }
        public int size { get; set; }
    }
}