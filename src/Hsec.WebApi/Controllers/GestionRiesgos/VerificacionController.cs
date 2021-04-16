
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using Hsec.Application.Verificaciones.Models;
using Hsec.Application.Verificaciones.Commands.CreateVerifiacion;
using Hsec.Application.Verificaciones.Commands.DeleteVerificacion;
using Hsec.Application.Verificaciones.Commands.UpdateVerificacion;
using Hsec.Application.Verificaciones.Queries.GetBuscarVerificacion;
using Hsec.Application.Verificaciones.Queries.GetSeguimientoVerificacion;
using Hsec.Application.Verificaciones.Queries.GetSeguimientoVerificaciones;
using Hsec.Application.Verificaciones.Queries.GetVerificacion;
using Microsoft.AspNetCore.Mvc;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.Files.Queries.GetFilesUpload;

namespace Hsec.WebApi.Controllers.GestionRiesgos
{    
    public class VerificacionController : ApiController
    {
        [HttpPost("Create"), DisableRequestSizeLimit]
        public async Task<ActionResult<string>> Create([FromForm] CreateVerifiacionVM data)
        {
            var file = Request.Form.Files;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<VerificacionDto>(data.JSONVerificacion);

            if (String.IsNullOrEmpty(data.JSONPlanAccion)) data.JSONPlanAccion = "[]";
            var planes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlanVM>>(data.JSONPlanAccion);

            return await Mediator.Send(new CreateVerificacionCommand() { data = obj, planAccion = planes, Files = file });
        }

        [HttpPost("{idVerificacion}/update"), DisableRequestSizeLimit]
        public async Task<ActionResult> Update([FromForm] UpdateVerificacionVM data, string idVerificacion)
        {
            var file = Request.Form.Files;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<VerificacionDto>(data.JSONVerificacion);
            if (obj.CodVerificacion != idVerificacion) return new BadRequestResult();

            if (String.IsNullOrEmpty(data.JSONPlanAccion)) data.JSONPlanAccion = "[]";
            var planes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlanVM>>(data.JSONPlanAccion);

            if (String.IsNullOrEmpty(data.JSONFile)) data.JSONFile = "[]";
            var updateFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FilesUploadOneVM>>(data.JSONFile);

            await Mediator.Send(new UpdateVerificacionCommand() { data = obj, planAccion = planes, newFiles = file, updateFiles = updateFiles }); ;

            return new NoContentResult();
        }

        [HttpGet("{idVerificacion}")]
        public async Task<ActionResult<GetVerificacionVM>> Get(string idVerificacion)
        {
            return await Mediator.Send(new GetVerificacionQuery() { CodVerificacion = idVerificacion });
        }

        [HttpGet("{idVerificacion}/remove")]
        public async Task<ActionResult> Remove(string idVerificacion)
        {
            if (idVerificacion == null) return new BadRequestResult();
            await Mediator.Send(new DeleteVerificacionCommand() { Codigo = idVerificacion });
            return new NoContentResult();
        }
        [HttpPost("Buscar")]
        public async Task<ActionResult<BuscarVerificacionVM>> Get(GetBuscarInsidentesQuery obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpGet("seguimiento/{id_modulo}/{id_persona}/{id_anio}/{id_mes}")]
        public async Task<ActionResult<int>> seguimiento(string id_modulo,string id_persona,int id_anio,int id_mes)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var codPersona = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;
            var usuario = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;
            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;
            return await Mediator.Send(new GetSeguimientoVerificacion() { 
                    modulo = id_modulo,
                    persona = id_persona,
                    anio = id_anio,
                    mes = id_mes,
                    usuario = usuario,
                    rol = rol
                });
        }

        [HttpPost("seguimiento")]
        public async Task<ActionResult<List<Tuple<string, int>>>> seguimiento(GetSeguimientoVerificaciones seg)
        {
            return await Mediator.Send(seg);
        }
    }
}
