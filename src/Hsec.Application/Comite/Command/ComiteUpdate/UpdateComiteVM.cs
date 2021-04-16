using Microsoft.AspNetCore.Http;

namespace Hsec.Application.Comite.Commands.ComiteUpdate
{
    public class UpdateComiteVM
    {
        public string data { get; set; }
        public IFormFileCollection Files { get; set; }
        public string UpdateFiles { get; set; }
    }
}
