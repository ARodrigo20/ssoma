using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Incidentes.Commands.UpdateIncidente
{
    public class UpdateIncidenteVM
    {
        public string JSONIncidente { get; set; }
        public string JSONPlanAccion { get; set; }
        public string JSONFile { get; set; }
        public IFormFileCollection Files { get; set; }

    }
}
