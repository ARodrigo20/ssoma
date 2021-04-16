using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Hsec.Application.Inspeccion.Commands.CreateInspeccion;
using Hsec.Application.Inspeccion.Commands.UpdateInspeccion;
using Hsec.Application.Inspeccion.Commands.DeleteInspeccion;
using Hsec.Application.Inspeccion.Queries.SearchInspeccion;
using Hsec.Application.Inspeccion.Queries.GetInspeccion;
using Hsec.Application.Inspeccion.Queries.GetSeguimiento;
using Hsec.Application.Inspeccion.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Hsec.Application.Common.Models;
using System;
using Hsec.Application.Files.Queries.GetFilesUpload;

namespace Hsec.WebApi.Controllers.Inspecciones
{
    public class InspeccionController : ApiController
    {
        [HttpPost("Create"), DisableRequestSizeLimit]
        public async Task<ActionResult<Unit>> Create([FromForm]CreateInspeccionVM data)
        {

            var files = Request.Form.Files;

            InspeccionDto inspeccion = null;
            if(data.inspeccion != null)
            {
                inspeccion = Newtonsoft.Json.JsonConvert.DeserializeObject<InspeccionDto>(data.inspeccion);
            }
            List<EquipoDto> equipo = null;
            if (data.equipo != null)
            {
                equipo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EquipoDto>>(data.equipo);
            }
            List<AtendidosDto> atendidos = null;
            if (data.atendidos != null)
            {
                atendidos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AtendidosDto>>(data.atendidos);
            }
            List<DetalleInspeccionDto> observaciones = null;
            if (data.observaciones != null)
            {
                observaciones = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DetalleInspeccionDto>>(data.observaciones);
            }
            List<InspeccionAnalisisCausaDto> analisisCausa = null;
            if (data.analisisCausa != null)
            {
                analisisCausa = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InspeccionAnalisisCausaDto>>(data.analisisCausa);
            }

            //List<PlanAccionVM> planes = null;
            //if (data.planes != null)
            //{
            //    planes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlanAccionVM>>(data.planes);
            //}

            //var planes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlanAccionDto>>(data.planes);

            return await Mediator.Send(new CreateInspeccionCommand() { Inspeccion = inspeccion, Equipo = equipo, Atendidos = atendidos, Observaciones = observaciones, AnalisisCausa = analisisCausa, Files = files });
        }

        [HttpPost("{codInspeccion}/Update"), DisableRequestSizeLimit]
        public async Task<ActionResult<Unit>> Update([FromForm]UpdateInspeccionVM data, string codInspeccion)
        {
            var files = Request.Form.Files;

            InspeccionDto inspeccion = null;
            if (data.inspeccion != null)
            {
                inspeccion = Newtonsoft.Json.JsonConvert.DeserializeObject<InspeccionDto>(data.inspeccion);
            }
            List<EquipoDto> equipo = null;
            if (data.equipo != null)
            {
                equipo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EquipoDto>>(data.equipo);
            }
            List<AtendidosDto> atendidos = null;
            if (data.atendidos != null)
            {
                atendidos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AtendidosDto>>(data.atendidos);
            }
            List<DetalleInspeccionDto> observaciones = null;
            if (data.observaciones != null)
            {
                observaciones = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DetalleInspeccionDto>>(data.observaciones);
            }
            List<InspeccionAnalisisCausaDto> analisisCausa = null;
            if (data.analisisCausa != null)
            {
                analisisCausa = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InspeccionAnalisisCausaDto>>(data.analisisCausa);
            }
            List<FilesUploadOneVM> updateFiles = null;
            if (data.JSONFile != null)
            {
                updateFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FilesUploadOneVM>>(data.JSONFile);
            }

            return await Mediator.Send(new UpdateInspeccionCommand() { Inspeccion = inspeccion, Equipo = equipo, Atendidos = atendidos, Observaciones = observaciones, AnalisisCausa = analisisCausa, Files = files, UpdateFiles = updateFiles });
        }

        [HttpGet("{codInspeccion}/Delete")]
        public async Task<ActionResult<Unit>> Delete(string codInspeccion)
        {
            if (string.IsNullOrEmpty(codInspeccion)) throw new ValidationException("codInspeccion no valido");
            return await Mediator.Send(new DeleteInspeccionCommand() { CodInspeccion = codInspeccion });
        }

        [HttpGet("{codInspeccion}")]
        public async Task<ActionResult<GetInspeccionVM>> Get(string codInspeccion)
        {
            if (string.IsNullOrEmpty(codInspeccion)) throw new ValidationException("codInspeccion no valido");
            return await Mediator.Send(new GetInspeccionQuery() { CodInspeccion = codInspeccion });
        }

        [HttpPost("Buscar")]
        public async Task<ActionResult<SearchInspeccionVM>> GetBuscar(SearchInspeccionQuery obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpGet("seguimiento/{id_modulo}/{id_persona}/{id_anio}/{id_mes}")]
        public async Task<ActionResult<int>> seguimiento(string id_modulo, string id_persona, int id_anio, int id_mes)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var codPersona = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;
            var usuario = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;
            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;
            return await Mediator.Send(new GetSeguimientoQuery() { 
                modulo = id_modulo, persona = id_persona, anio = id_anio, mes = id_mes, usuario = usuario, rol = rol 
                });
        }

        [HttpPost("seguimiento")]
        public async Task<ActionResult<List<Tuple<string, int>>>> seguimiento(GetSeguimientosQuery seg)
        {
            return await Mediator.Send(seg);
        }
    }
}
