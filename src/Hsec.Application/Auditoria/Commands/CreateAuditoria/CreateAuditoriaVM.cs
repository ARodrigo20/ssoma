using System.Collections.Generic;
using Hsec.Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Hsec.Application.Auditoria.Commands.CreateAuditoria
{
    public class CreateAuditoriaVM
    {
        public string JSONAuditoria { get; set; }
        public IFormFileCollection Files { get; set; }

    }
}