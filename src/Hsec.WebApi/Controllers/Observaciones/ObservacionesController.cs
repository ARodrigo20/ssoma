
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Models;
using Hsec.Application.Observacion.Commands.CreateObservacion;
using Hsec.Application.Observacion.Commands.DeleteObservacion;
using Hsec.Application.Observacion.Commands.UpdateObservacion;
using Hsec.Application.Observacion.Queries.GetObservacion;
using Hsec.Application.Observacion.Queries.GetObservacionesBuscar;
using Hsec.Application.Observacion.Queries.GetPortletGrafico;
using Hsec.Application.Observacion.Queries.GetResueltosVerCC;
using Hsec.Application.Observacion.Queries.GetSeguimientoObseracion;
using Hsec.Application.Observacion.Queries.GetSeguimientoObseraciones;
using Hsec.Domain.Enums;
//using Hsec.Application.TodoItems.Queries.GetTodos;
using Microsoft.AspNetCore.Mvc;
using Hsec.Application.Files.Queries.GetFilesUpload;

namespace Hsec.WebApi.Controllers.Observaciones
{    
    public class ObservacionesController : ApiController
    {
        [HttpPost("buscar")]
        public async Task<ActionResult<ObservacionesVM>> Get(GetObservacionesBuscarQuery obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpGet("{idObervacion}")]
        public async Task<ActionResult<ObservacionVM>> Get(string idObervacion)
        {
            if (idObervacion == null || idObervacion.Equals(""))
            {
                return BadRequest();
            }
            ObservacionVM vm = await Mediator.Send(new GetObservacionQuery() { CodObservacion = idObervacion });
            
            return vm;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<string>> Create([FromForm] FilesAndVM data)
        {
            var file = Request.Form;

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Application.Observacion.Commands.CreateObservacion.ObservacionDto>(data.dataJSON);
            obj.files = data.files.Files;
            int value = 0;
            if(obj.Tarea != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.Tarea.ToString();
                value++;
            }
            if (obj.Condicion != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.Condicion.ToString();
                value++;
            }
            if (obj.Comportamiento != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.Comportamiento.ToString();
                value++;
            }
            if (obj.IteraccionSeguridad != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.Iteraccion_Seguridad.ToString();
                value++;
            }
            if (obj.VerificacionControlCritico != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.VerificacionControlCritico.ToString();
                value++;
            }
            if (obj.Covid19 != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.Covid19.ToString();
                value++;
            }

            if (value != 1)
            {
                return new BadRequestResult();
            }
            else
            {
                return await Mediator.Send(new CreateObservacionCommand() { data = obj });
            }
        }
        
        [HttpPost("{idObservacion}/update")]
        public async Task<ActionResult> Update([FromForm] FilesAndVM data, string idObservacion)
        {
            if (idObservacion == null || idObservacion.Equals(""))
            {
                return BadRequest();
            }
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Application.Observacion.Commands.UpdateObservacion.ObservacionDto>(data.dataJSON);
            obj.files = data.files.Files;
            if(data.dataFilesJSON!=null&&!data.dataFilesJSON.Equals(""))
                obj.FilesSTR = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FilesUploadOneVM>>(data.dataFilesJSON);

            int value = 0;
            if (obj.Tarea != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.Tarea.ToString();

                value++;
            }
            if (obj.Condicion != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.Condicion.ToString();
                value++;
            }
            if (obj.Comportamiento != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.Comportamiento.ToString();
                value++;
            }
            if (obj.IteraccionSeguridad != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.Iteraccion_Seguridad.ToString();
                value++;
            }
            if (obj.VerificacionControlCritico != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.VerificacionControlCritico.ToString();
                value++;
            }
            if (obj.Covid19 != null)
            {
                //obj.CodTipoObservacion = TipoObservacion.Covid19.ToString();
                value++;
            }

            if (value != 1)
            {
                return new BadRequestResult();
            }

            obj.CodObservacion = (idObservacion);
            await Mediator.Send(new UpdateObservacionCommand() { data = obj });
            return NoContent();
        }

        [HttpGet("{idObservacion}/delete")]
        public async Task<ActionResult> Delete(string idObservacion)
        {
            await Mediator.Send(new DeleteObservacionCommand() { CodObservacion = idObservacion });
            return NoContent();
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
            return await Mediator.Send(new GetSeguimientoObseracion() { 
                    modulo = id_modulo,
                    persona = id_persona,
                    anio = id_anio,
                    mes = id_mes ,
                    usuario = usuario,
                    rol = rol
                });
        }

        [HttpPost("seguimiento")]
        public async Task<ActionResult<List<Tuple<string, int>>>> seguimiento(GetSeguimientoObseraciones seg)
        {
            return await Mediator.Send(seg);
        }

        [HttpGet("verificaionControlCritico")]
        public async Task<ActionResult<ResueltosVM>> GetVCC(string codPersona,string Anio,string mes)
        {
            return await Mediator.Send(new GetReueltosVerCCQuery(){
                CodPersona = codPersona,
                Anio = Anio,
                CodMes = mes
            });
            // return new Application.Observacion.Commands.CreateObservacion.ObservacionDto();
        }

        [HttpGet("verificaionControlCritico/covid19")]
        public async Task<ActionResult<ResueltosVM>> GetVCCCovid(string codPersona,string Anio,string mes)
        {
            return await Mediator.Send(new GetReueltosVerCCCovid19Query(){
                CodPersona = codPersona,
                Anio = Anio,
                CodMes = mes
            });
        }
        
        [HttpGet("portlet/meses")]
        public async Task<ActionResult<PortletMesesVM>> PortletMeses()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var codPersona = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;
            if(codPersona==null||codPersona.Equals("")) return new PortletMesesVM();
            return await Mediator.Send(new GetPortletGraficoQuery(){CodPersona = codPersona});
        }

        [HttpGet("Models")]
        public async Task<ActionResult<Application.Observacion.Commands.CreateObservacion.ObservacionDto>> GetModels()
        {
            return new Application.Observacion.Commands.CreateObservacion.ObservacionDto();
        }
    }
}
