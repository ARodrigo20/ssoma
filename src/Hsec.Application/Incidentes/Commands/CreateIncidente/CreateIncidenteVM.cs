using Microsoft.AspNetCore.Http;

namespace Hsec.Application.Incidentes.Commands.CreateIncidente
{
    public class CreateIncidenteVM
    {
        public string JSONIncidente { get; set; }
        public string JSONPlanAccion { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
