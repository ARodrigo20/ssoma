using System.Threading.Tasks;

using Hsec.Application.General.AnalisisCausas.Queries.GetAnalisisCausas;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Hsec.Application.Common.Models;
using Hsec.Application.General.AnalisisCausas.Queries.GetAnalisisCausaID;
using Hsec.Application.General.AnalisisCausas.Queries.GetAnalisisCausaPartner;
using Hsec.Application.General.AnalisisCausas.Commands.CreateAnalisisCausa;
using Hsec.Application.General.AnalisisCausas.Commands.UpdateAnalisisCausa;
using System.Collections.Generic;

namespace Hsec.WebApi.Controllers.General
{
    public class AnalisisCausaController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<GeneralCollection<AnalisisCausaDto>>> Get()
        {
            return await Mediator.Send(new GetAnalisisCausasAllQuery());
        }

        // GET api/<PersonaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnalisisCausaDto>> Get(string id)
        {
            return await Mediator.Send(new GetAnalisisCausaIDQuery() { CodAnalisis = id });
        }

        [HttpGet("GetHijos/{id}")]
        public async Task<ActionResult<List<AnalisisCausaVM>>> GetHijos(string id)
        {
            return await Mediator.Send(new GetAnalisisCausaPartnerQuery() { CodPadre = id });
        }

        // POST api/<PersonaController>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] CreateAnalisisCausaCommand value)
        {
            return await Mediator.Send(value);
        }
        [HttpPost("Update")]
        public async Task<ActionResult<Unit>> Update([FromBody] UpdateAnalisisCausaCommand value)
        {
            return await Mediator.Send(value);
        }

        [HttpGet("Delete/{id}")]
        public async Task<ActionResult<Unit>> Delete(string id)
        {
            return await Mediator.Send(new DeleteAnalisisCausaCommand() { CodAnalisis = id });
        }
    }
}
