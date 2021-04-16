using Hsec.Application.Common.Interfaces;
using Hsec.Application.PlanAccion.Commands.CreateLevantamientoTareas;
using Microsoft.AspNetCore.Http;

namespace Hsec.Infrastructure.Services
{
    public class FileUploadLevTareaService : IFileLevTareaService
    {
        public CreateLevTareaDto imgUpload(IFormCollection form)
        {
            CreateLevTareaDto listaForm = new CreateLevTareaDto();

            foreach (var item in form)
            {
                if (item.Key == "Json")
                {
                    listaForm = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateLevTareaDto>(item.Value.ToString());
                }
            }
            return listaForm;
        }
    }
}
