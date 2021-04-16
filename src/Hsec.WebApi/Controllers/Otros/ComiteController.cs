using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hsec.Application.Comite.Command.ComiteCreate;
using Hsec.Application.Comite.Command.ComiteDelete;
using Hsec.Application.Comite.Command.ComiteUpdate;
using Hsec.Application.Comite.Commands.ComiteCreate;
using Hsec.Application.Comite.Commands.ComiteUpdate;
using Hsec.Application.Comite.Models;
using Hsec.Application.Comite.Queries.ComiteGetByCod;
using Hsec.Application.Comite.Queries.ComiteSearch;
using Hsec.Application.Comite.Queries.GetSeguimiento;
using Hsec.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hsec.Application.Files.Queries.GetFilesUpload;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hsec.WebApi.Controllers.Otros
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComiteController : ApiController
    {

        [HttpPost("create"), DisableRequestSizeLimit]
        public async Task<ActionResult<Unit>> Create([FromForm] CreateComiteVM dat)
        {
            var file = Request.Form.Files;

            CreateComiteDto _comite = null;
            if (dat.data != null)
            {
                _comite = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateComiteDto>(dat.data);
            }

            return await Mediator.Send(new ComiteCreateCommand() { data = _comite, Files = file});
        }

        [HttpPost("search")]
        public async Task<ActionResult<ComiteVM>> Post([FromBody] ComiteSearchQuery value)
        {
            return await Mediator.Send(value);
        }

        [HttpGet("getbycod/{cod}")]
        public async Task<ActionResult<ComiteGetDto>> Get(string cod)
        {
            return await Mediator.Send(new ComiteGetByCodQuery() { CodComite = cod });
        }

        [HttpGet("delete/{cod}")]
        public async Task<ActionResult<Unit>> Get(string cod, string delete)
        {
            return await Mediator.Send(new ComiteDeleteCommand() {  CodComite = cod });
        }

        [HttpPost("update"), DisableRequestSizeLimit]
        public async Task<ActionResult<Unit>> Update([FromForm] UpdateComiteVM dat)
        {
            var files = Request.Form.Files;

            CreateComiteDto comite = null;
            if (dat.data != null)
            {
                comite = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateComiteDto>(dat.data);
            }
            List<FilesUploadOneVM> updateFiles = null;
            if (dat.UpdateFiles != null)
            {
                updateFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FilesUploadOneVM>>(dat.UpdateFiles);
            }

            return await Mediator.Send(new ComiteUpdateCommand() { data = comite, Files = files, UpdateFiles = updateFiles });
        }

        [HttpGet("Models")]
        public async Task<ActionResult<CreateComiteDto>> GetModels()
        {
            return new CreateComiteDto();
        }

        [HttpPost("seguimiento")]
        public async Task<ActionResult<List<Tuple<string, int>>>> seguimiento(GetSeguimientoQuery seg)
        {
            return await Mediator.Send(seg);
        }

        [HttpGet("seguimiento/{id_modulo}/{id_persona}/{id_anio}/{id_mes}")]
        public async Task<ActionResult<int>> seguimiento(string id_modulo, string id_persona, int id_anio, int id_mes)
        {
            return await Mediator.Send(new GetSeguimientoIndividualQuery()
            {
                modulo = id_modulo,
                persona = id_persona,
                anio = id_anio,
                mes = id_mes
            });
        }
    }
}
