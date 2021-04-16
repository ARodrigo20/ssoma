using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hsec.Application.ToleranciaCero.Command.ToleranciaCeroInsert;
using Hsec.Application.ToleranciaCero.Queries.ToleranciaCeroSearch;
using Hsec.Application.ToleranciaCero.Command.ToleranciaCeroUpdate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hsec.Application.ToleranciaCero.Queries.ToleranciaCeroGetCod;
using Hsec.Application.ToleranciaCero.Command.ToleranciaCeroDelete;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hsec.WebApi.Controllers.Otros
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToleranciaCeroController : ApiController
    {

        // POST api/<ToleranciaCeroController>
        [HttpPost("create")]
        public async Task<ActionResult<Unit>> Post([FromBody] ToleranciaCeroInsertCommand value)
        {
            return await Mediator.Send(value);
        }

        [HttpPost("search")]
        public async Task<ActionResult<ToleranciaCeroModel>> Post([FromBody] ToleranciaCeroSearchCommand value)
        {
            return await Mediator.Send(value);
        }

        [HttpGet("getcod/{cod}")]
        public async Task<ActionResult<Application.ToleranciaCero.Queries.ToleranciaCeroGetCod.ToleranciaCeroGetDto>> Get(string cod)
        {
            return await Mediator.Send(new ToleranciaCeroGetCodQuery() { CodTolCero = cod });
        }

        [HttpGet("delete/{cod}")]
        public async Task<ActionResult<Unit>> Get(string cod,string delete)
        {
            return await Mediator.Send(new ToleranciaCeroDeleteCommand() { CodTolCero = cod });
        }

        [HttpPost("update")]
        public async Task<ActionResult<Unit>> Post([FromBody] ToleranciaCeroUpdateCommand value)
        {
            return await Mediator.Send(value);
        }

        //// GET: api/<ToleranciaCeroController>
        //[HttpGet]
        //public async Task<ActionResult<ToleranciaCeroModel>> Get()
        //{
        //    return await Mediator.Send(new ToleranciaCeroSearchCommand());
        //}

        //// PUT api/<ToleranciaCeroController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ToleranciaCeroController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
