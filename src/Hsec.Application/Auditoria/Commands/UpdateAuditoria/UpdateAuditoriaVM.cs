using System.Collections.Generic;
using Hsec.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Hsec.Application.Files.Queries.GetFilesUpload;

namespace Hsec.Application.Auditoria.Commands.UpdateAuditoria
{
    public class UpdateAuditoriaVM
    {
        public string JSONAuditoria { get; set; }
        public string planAccion { get; set; }
        public IFormFileCollection newFiles { get; set; }
        public List<FilesUploadOneVM> updateFiles { get; set; }
    }
}