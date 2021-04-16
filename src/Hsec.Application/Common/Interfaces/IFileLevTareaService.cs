using Hsec.Application.PlanAccion.Commands.CreateLevantamientoTareas;
using Microsoft.AspNetCore.Http;

namespace Hsec.Application.Common.Interfaces
{
    public interface IFileLevTareaService
    {
        public CreateLevTareaDto imgUpload(IFormCollection file);
    }
}
