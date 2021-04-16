using Microsoft.AspNetCore.Http;

namespace Hsec.Application.Comite.Commands.ComiteCreate
{
    public class CreateComiteVM
    {
        public string data { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
