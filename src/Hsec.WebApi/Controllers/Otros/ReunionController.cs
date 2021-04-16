using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hsec.Application.Reunion.Command.ReunionCreate;
using Hsec.Application.Reunion.Command.ReunionDelete;
using Hsec.Application.Reunion.Command.ReunionUpdate;
using Hsec.Application.Reunion.Queries.GetSeguimiento;
using Hsec.Application.Reunion.Queries.ReunionGetByCod;
using Hsec.Application.Reunion.Queries.ReunionSearch;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hsec.WebApi.Controllers.Otros
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReunionController : ApiController
    {
        [HttpPost("create")]
        public async Task<ActionResult<Unit>> Post([FromBody] ReunionCreateCommand value)
        {
            return await Mediator.Send(value);
        }

        [HttpPost("search")]
        public async Task<ActionResult<ReunionVM>> Post([FromBody] ReunionSearchQuery value)
        {
            return await Mediator.Send(value);
        }

        [HttpGet("getbycod/{cod}")]
        public async Task<ActionResult<ReunionGetDto>> Get(string cod)
        {
            return await Mediator.Send(new ReunionGetByCodQuery() {  CodReunion = cod });
        }

        [HttpGet("delete/{cod}")]
        public async Task<ActionResult<Unit>> Get(string cod, string delete)
        {
            return await Mediator.Send(new ReunionDeleteCommand() { CodReunion = cod });
        }

        [HttpPost("update")]
        public async Task<ActionResult<Unit>> Post([FromBody] ReunionUpdateCommand value)
        {
            return await Mediator.Send(value);
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
