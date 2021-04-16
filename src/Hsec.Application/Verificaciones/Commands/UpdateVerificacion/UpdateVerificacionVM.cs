using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Verificaciones.Commands.UpdateVerificacion
{
    public class UpdateVerificacionVM
    {
        public string JSONVerificacion { get; set; }
        public string JSONPlanAccion { get; set; }
        public string JSONFile { get; set; }
        public IFormFileCollection Files { get; set; }

    }
}
