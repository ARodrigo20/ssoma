
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hsec.Application.Auditoria.Commands.CreateAuditoria;
using Hsec.Application.Auditoria.Commands.UpdateAuditoria;
using Hsec.Application.Auditoria.Queries.GetAuditoria;
using Hsec.Application.Auditoria.Queries.GetBuscarHallazgosAuditoria;
using Hsec.Application.Auditoria.Queries.GetSeguimientoAuditoria;
using Hsec.Application.Auditoria.Models;
using Hsec.Application.Incidentes.Queries.GetBuscarInsidentes;
using Hsec.Application.Incidentes.Commands.DeleteIncidente;
using Microsoft.AspNetCore.Mvc;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.WebApi.Controllers.Auditoria
{    
    public class AuditoriaController : ApiController
    {
        [HttpPost("Buscar")]
        public async Task<ActionResult<BuscarAuditoriaVM>> GetBuscarAuditoria(GetBuscarAuditoriaQuery obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpGet("BuscarHallazgos/{IdAuditoria}/{PaginaTamanio}/{Pagina}")]
        public async Task<ActionResult<BuscarHallazgosAuditoriaVM>> GetBuscarHallazgo(string IdAuditoria,int PaginaTamanio,int Pagina)
        {
            return await Mediator.Send(new GetBuscarHallazgosAuditoriaQuery() { CodAuditoria = IdAuditoria,Pagina = Pagina, PaginaTamanio = PaginaTamanio });
        }

        [HttpPost("Create"), DisableRequestSizeLimit]
        public async Task<ActionResult<string>> PostCreate([FromForm] CreateAuditoriaVM data)
        {
            var file = Request.Form.Files;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<AuditoriaDto>(data.JSONAuditoria);
            
            // if (String.IsNullOrEmpty(data.planAccion)) data.planAccion = "[]";
            // var planes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlanAccionVM>>(data.planAccion);

            return await Mediator.Send(new CreateAuditoriaCommand() { data = obj, Files = file });
        }

        [HttpGet("{IdAuditoria}")]
        public async Task<ActionResult<GetAuditoriaVM>> PostCreate(string IdAuditoria)
        {
            return await Mediator.Send(new GetAuditoriaQuery() { CodAuditoria = IdAuditoria });
        }

        [HttpGet("seguimiento/{id_modulo}/{id_persona}/{id_anio}/{id_mes}")]
        public async Task<ActionResult<int>> seguimiento(string id_modulo,string id_persona,int id_anio,int id_mes)
        {
            return await Mediator.Send(new GetSeguimientoAuditoria() { 
                    modulo = id_modulo,
                    persona = id_persona,
                    anio = id_anio,
                    mes = id_mes 
                });
        }

        [HttpPost("Update/{IdAuditoria}"), DisableRequestSizeLimit]
        public async Task<ActionResult> PostUpdate([FromForm] UpdateAuditoriaVM data)
        {
            var file = Request.Form.Files;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<AuditoriaDto>(data.JSONAuditoria);
            
            if (String.IsNullOrEmpty(data.planAccion)) data.planAccion = "[]";
            var planes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlanVM>>(data.planAccion);
            await Mediator.Send(new UpdateAuditoriaCommand() { data = obj,  newFiles = data.newFiles, updateFiles = data.updateFiles });
            return NoContent();
        }

        [HttpGet("Remove/{IdAuditoria}")]
        public async Task<ActionResult> PostRemove(string IdAuditoria)
        {
            await Mediator.Send(new DeleteAuditoriaCommand() { Codigo = IdAuditoria });
            return NoContent();
        }

    }
}
