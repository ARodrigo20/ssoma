using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Verificaciones.Commands.CreateVerifiacion
{
    public class CreateVerifiacionVM
    {
        public string JSONVerificacion { get; set; }
        public string JSONPlanAccion { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
