using Microsoft.AspNetCore.Http;

namespace Hsec.Application.VerificacionControlCritico.Commands.CreateVerificacionControlCritico
{
    public class CreateVerConCriVM
    {
        public string VerConCri { get; set; }
        public IFormFileCollection Files {get;set;}
    }
}