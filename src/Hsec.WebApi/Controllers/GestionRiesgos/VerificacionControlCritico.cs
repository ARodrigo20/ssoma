
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hsec.Application.Common.Models;
using Hsec.Application.VerificacionControlCritico.Commands.CreateVerificacionControlCritico;
using Hsec.Application.VerificacionControlCritico.Commands.DeleteVerificacionControlCritico;
using Hsec.Application.VerificacionControlCritico.Commands.UpdateVerificacionControlCritico;
using Hsec.Application.VerificacionControlCritico.Queries.BuscarVerificacionControlCritico;
using Hsec.Application.VerificacionControlCritico.Queries.GetVerificacionControlCritico;
using Microsoft.AspNetCore.Mvc;
using Hsec.Application.VerificacionControlCritico.Queries.BuscarVerificacionControlCriticoCartilla;
using Hsec.Application.VerificacionControlCritico.Queries.AvancePorCartilla;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.Files.Queries.GetFilesUpload;

namespace Hsec.WebApi.Controllers.GestionRiesgos
{
    public class VerificacionControlCritico : ApiController
    {
        // [HttpPost("Buscar")]
        // public async Task<ActionResult<BuscarVerificacionCCVM>> GetBuscar(BuscarVerificacionControlCcriticoQuery obj)
        // {
        //     return await Mediator.Send(obj);
        // }
        [HttpPost("Buscar")]
        public async Task<ActionResult<BuscarVerificacionCCCartillaVM>> GetBuscarAvance(BuscarVerificacionControlCcriticoCartillaQuery obj)
        {
            return await Mediator.Send(obj);
        }
        
        [HttpGet("AvancePorCartilla")]
        public async Task<ActionResult<AvancePorCartillaVM>> GetBuscarAvance2(string CodPersona,string CodReferencia,string Anio,string CodMes)
        {
            return await Mediator.Send(new AvancePorCartillaQuery(){CodPersona=CodPersona,CodReferencia=CodReferencia,Anio=Anio,CodMes=CodMes});
        }
        
        [HttpPost("Create"), DisableRequestSizeLimit]
        public async Task<ActionResult<string>> Remove([FromForm]CreateVerConCriVM data){

            var file = Request.Form.Files;
            
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Application.VerificacionControlCritico.Commands.CreateVerificacionControlCritico.VerificacionControlCriticoDto>(data.VerConCri);

            // var files = data.files.Files;
            // if(data.dataFilesJSON!=null&&!data.dataFilesJSON.Equals(""))
            //     obj.FilesSTR = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileVM>>(data.dataFilesJSON);

            return await Mediator.Send(new CreateVerificacionControlCriticoCommand(){verConCrit = obj, Files = file});
        }

        [HttpPost("{idVerificacion}/update"), DisableRequestSizeLimit]
        public async Task<ActionResult> Update([FromForm] UpdateVerConCriVM data, string idVerificacion)
        {
            var file = Request.Form.Files;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Application.VerificacionControlCritico.Commands.UpdateVerificacionControlCritico.VerificacionControlCriticoDto>(data.VerConCri);
            if (obj.CodigoVCC != idVerificacion) return new BadRequestResult();

            if (String.IsNullOrEmpty(data.JSONFile)) data.JSONFile = "[]";
            var updateFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FilesUploadOneVM>>(data.JSONFile);

            await Mediator.Send(new UpdateVerificacionControlCriticoCommand() { verConCrit = obj, Files = file, UpdateFiles = updateFiles }); ;

            return new NoContentResult();
        }

        [HttpGet("{idVerificacion}/remove")]
        public async Task<ActionResult> Remove(string idVerificacion)
        {
            if (idVerificacion == null) return new BadRequestResult();
            await Mediator.Send(new DeleteVerificacionControlCriticoCommand() { CodVCC = idVerificacion });
            return new NoContentResult();
        }

        [HttpGet("{idVerificacion}")]
        public async Task<ActionResult<GetVerConCriVM>> Get(string idVerificacion)
        {
            return await Mediator.Send(new GetVerificacionControlCriticoQuery() { Codigo = idVerificacion });
        }
        
    }
}