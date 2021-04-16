using Hsec.Application.PlanAccion.Commands.CreateLevantamientoTareas;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.DeletePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.UpdateLevantamientoTareas;
using Hsec.Application.PlanAccion.Commands.RechazarLevantamientoTarea;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;
using Hsec.Application.PlanAccion.Queries.GetLevatamientoTareas;
using Hsec.Application.PlanAccion.Queries.GetPersonasJerarquia;
using Hsec.Application.PlanAccion.Queries.GetPlanAccionDocReferencia;
using Hsec.Application.PlanAccion.Queries.GetPlanDeAccion;
using Hsec.Application.PlanAccion.Queries.GetPorlet;
using Hsec.Application.PlanAccion.Commands.DeleteDocRefHsec;
using Hsec.Application.Files.Commands.DeleteFileDocRef;
using Hsec.Application.Files.Commands.DeleteFiles;
using Hsec.Application.Files.Commands.UpdateFiles;
using Hsec.Application.Files.Commands.CreateFiles;
using Hsec.Application.Files.Queries.GetFilesUpload;
using Hsec.Application.Files.Queries.GetFileUploadCodRef;
using Hsec.Application.Files.Queries.GetImagenPorDniQuery;
using Hsec.Application.ValidadorFiles.Commands;
using Hsec.Application.Files.Queries.GetFileUpload;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;


namespace Hsec.WebApi.Controllers.PlanAccion
{
    public class AccionController : ApiController
    {
        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<Unit>> CreatePlanDeAccion(IList<PlanVM> planesAccion)
        {
            return await Mediator.Send(new CreatePlanAccionCommand() { planes = planesAccion });
        }

        [Route("Delete/{codAccion}")]
        [HttpGet]
        public async Task<ActionResult<Unit>> DeletePlanDeAccion(int codAccion)
        {
            return await Mediator.Send(new DeletePlanAccionCommand() { CodAccion = codAccion });
        }
        [Route("Delete/CodDocRef/{NroDocReferencia}")]
        [HttpGet]
        public async Task<ActionResult<Unit>> DeletePlanDeAccionCodRef(string NroDocReferencia)
        {
            return await Mediator.Send(new DeleteDocRefCommand() { NroDocReferencia = NroDocReferencia });
        }

        [Route("Update")]
        [HttpPost]
        public async Task<ActionResult<Unit>> UpdatePlanDeAccion(IList<PlanVM> planesAccion)
        {
            return await Mediator.Send(new UpdatePlanAccionCommand() { planes = planesAccion });
        }

        [HttpGet("Porlet/GetEstado")]
        public async Task<List<Tuple<string,int>>> GetEstadoPortlet()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var codPersona = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;
            return await Mediator.Send(new GetEstadoPorletQuery() {CodPersona= codPersona });
        }

        [HttpGet("Porlet/GetPlanes")]
        public async Task<List<PlanesDto>> GetPlanesPortlet()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var codPersona = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;
            return await Mediator.Send(new GetPlanesPorletQuery() { CodPersona = codPersona });
        }

        [Route("Get/Filtrado")]
        [HttpPost]
        public async Task<PlanAccionVM> GetPlanDeAccion(GetPlanAccionFiltradoDto plan)
        {
            return await Mediator.Send(new GetPlanAccionQuery() { plan = plan });
        }

        [Route("Get/Filtrado/Planes/Jerarquia")]
        [HttpPost]
        public async Task<JerarquiaVM> GetFiltradoPlanesJerarquia(GetPlanAccionFiltradoDto request)
        {
            return await Mediator.Send(new GetPlanAccionCodPosicionQuery() { plan = request });
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<PlanAccionMaestroDto> GetPlanDeAccion(int id)
        {
            return await Mediator.Send(new GetPlanAccionIDQuery(id));
        }

        [Route("Plan/Get/CodDocReferencia")]
        [HttpGet]
        public async Task<PlanAccionVM> GetPlanDeAccionPorDocReferencia(string codRef, string codSub, string codTabla)
        {
            return await Mediator.Send(new GetPlanAccionDocReferenciaQuery() { DocReferencia = codRef, DocSubReferencia = codSub, TablaReferencia = codTabla });
        }

        [Route("File/CreateLevTarea")]
        [HttpPost]
        public async Task<PlanCompVM> CreateFileLevTarea()
        {
            var file = Request.Form;
            return await Mediator.Send(new CreateLevTareaCommand() { File = (IFormCollection)file });
        }

        [Route("File/UpdateLevTarea")]
        [HttpPost]
        public async Task<PlanCompVM> UpdateFileLevTarea()
        {
            var file = Request.Form;
            return await Mediator.Send(new UpdateLevTareaCommand() { File = (IFormCollection)file });
        }

        [Route("File/GetLevTarea/{codAccion}")]
        [HttpGet]
        public async Task<PlanCompVM> GetFileLevTarea(int codAccion)
        {
            return await Mediator.Send(new GetLevTareasQuery() { codAccion = codAccion });
        }

        [HttpGet("RechazarLevTarea/{codAccion}")]
        public async Task<ActionResult<Unit>> RechazarLevTarea(int codAccion)
        {
            return await Mediator.Send(new RechazarLevantamientoTareaCommand() { CodAccion = codAccion });
        }

        // files


        [HttpPost("UploadUserFile")] //,DisableRequestSizeLimit
        [RequestSizeLimit(524288000)] //500Mbs
        public async Task<ActionResult> UpdateUserPhoto()
        {
            var file = Request.Form;
            return Ok(await Mediator.Send(new CreateFileCommand { File = (IFormCollection)file }));
        }

        [Route("Get/Files")]
        [HttpGet]
        public async Task<FileUploadAllVM> GetFileAll()
        {
            return await Mediator.Send(new GetFilesAllQuery());
        }

        [Route("Update/Files")] // update and delete of List<Tfiles>
        [HttpPost]
        public async Task<Unit> UpdateFile(FileListUpdateVM Files)
        {
            return await Mediator.Send(new UpdateFilesCommand() { doc = Files });
        }

        [Route("Update/File")]
        [HttpPost]
        public async Task<Unit> UpdateFile(UpdateFileCommand file)
        {
            return await Mediator.Send(new UpdateFileCommand() { CorrelativoArchivos = file.CorrelativoArchivos, Descripcion = file.Descripcion });
        }

        [Route("Delete/File/{codFile}")]
        [HttpGet]
        public async Task<Unit> DeleteFile(int codFile)
        {
            return await Mediator.Send(new DeleteFileCommand() { CorrelativoArchivos = codFile });
        }

        [Route("Delete/File/CodDocRef/{codFile}")]
        [HttpGet]
        public async Task<Unit> DeleteFileCodRef(string codFile)
        {
            return await Mediator.Send(new DeleteFileDocRefCommand() { NroDocReferencia = codFile });
        }

        [Route("File/Get/CodDocReferencia")]
        [HttpGet]
        public async Task<FileUploadAllVM> GetFilePorDocReferencia(string codRef, string codSub, string codTabla)
        {
            return await Mediator.Send(new GetFileUploadCodRefQuery()
            {
                NroDocReferencia = codRef,
                NroSubDocReferencia = codSub,
                CodTablaRef = codTabla
            });
        }

        //Este Servicio se enfoca en capacitaciones !
        [Route("File/UpdateValFilesRevisado")]
        [HttpPost]
        public async Task<Unit> UpdateValFiles(UpdateValidadorFilesCommand VM)
        {
            return await Mediator.Send(VM);
        }

        // Servicio de descarga de archivos
        [AllowAnonymous]
        [Route("File/CreateValFiles")]
        [HttpGet]
        public async Task<ActionResult> CreateValFiles(string nroDocReferencia, string codPersona, int codArchivo)
        {
            var file = await Mediator.Send(new CreateValidadorFilesCommand()
            {
                codArchivo = codArchivo,
                codPersona = codPersona,
                nroDocReferencia = nroDocReferencia
            });

            var fileContent = new FileContentResult(file.archivoData, System.Net.Mime.MediaTypeNames.Application.Octet);

            fileContent.FileDownloadName = file.nombre;

            return fileContent;
        }
        [AllowAnonymous]
        [HttpGet("File/{id}/{name}.{ext}")]
        public async Task<ActionResult> GetFile(int id, string name, string ext)
        {
            var file = await Mediator.Send(new GetFileQuery { CorrelativoArchivos = id });
            return new FileContentResult(file.archivoData, file.tipoArchivo);
        }
        [AllowAnonymous]
        [HttpGet("File/{id}/")]
        public async Task<ActionResult> GetFile(int id)
        {
            var file = await Mediator.Send(new GetFileQuery { CorrelativoArchivos = id });
            if (file.tipoArchivo.Contains("image") || file.tipoArchivo.Contains("video") || file.tipoArchivo.Contains("pdf")) return new FileContentResult(file.archivoData, file.tipoArchivo);
            else
            {
                var fileContent = new FileContentResult(file.archivoData, file.tipoArchivo);
                fileContent.FileDownloadName = file.nombre;
                return fileContent;
            }
        }

        [AllowAnonymous]
        [HttpGet("File/Preview/{id}/")]
        public async Task<ActionResult> GetFilePreview(int id)
        {
            var file = await Mediator.Send(new GetFilePreviewQuery { CorrelativoArchivos = id });
            return new FileContentResult(file, System.Net.Mime.MediaTypeNames.Image.Jpeg);
        }

        [AllowAnonymous]
        [HttpGet("File/Persona/{dni}")]
        public async Task<ActionResult> GetImagenPorDni(string dni)
        {
            var file = await Mediator.Send(new GetImagenPorDniQuery { Dni = dni });
            return new FileContentResult(file, System.Net.Mime.MediaTypeNames.Image.Jpeg);

        }

        [AllowAnonymous]
        [HttpGet("File/Process/")]
        public async Task<Unit> ProcesarImagenPreview()
        {
            return await Mediator.Send(new ProcessImageCommand());

        }

    }
}