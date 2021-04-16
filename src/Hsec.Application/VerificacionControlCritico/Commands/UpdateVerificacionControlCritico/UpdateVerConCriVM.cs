using Microsoft.AspNetCore.Http;

namespace Hsec.Application.VerificacionControlCritico.Commands.UpdateVerificacionControlCritico
{
    public class UpdateVerConCriVM
    {
        public string VerConCri { get; set; }
        public string JSONFile {get;set;}
        public IFormFileCollection file {get;set;}
    }
}