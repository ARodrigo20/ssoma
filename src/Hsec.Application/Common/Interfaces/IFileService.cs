using Hsec.Application.Files.Commands.CreateFiles;
using Hsec.Domain.Entities.PlanAccion;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Hsec.Application.Common.Interfaces
{
    public interface IFileService
    {
        public CreateFileCommand imgUpload(IFormCollection file);
        public Task<byte[]> getImagePreview(TFile file);
    }
}
