using Microsoft.AspNetCore.Http;

namespace Hsec.Application.Inspeccion.Commands.UpdateInspeccion
{
    public class UpdateInspeccionVM
    {
        public string inspeccion { get; set; }
        public string equipo { get; set; }
        public string atendidos { get; set; }
        public string observaciones { get; set; }
        public string analisisCausa { get; set; }
        public string JSONFile { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
