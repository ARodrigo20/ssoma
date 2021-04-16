using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Incidentes.Models
{
    public class MaestroDataVM
    {
        public string codTabla { get; set; }
        public HashSet<CampoDto> tipos { get; set; }
    }
}
