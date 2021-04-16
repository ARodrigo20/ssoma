

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Hsec.Application.General.ControlCritico.Commands.CreateControlCritico;
using Hsec.Application.General.ControlCritico.Commands.DeleteControlCritico;
using Hsec.Application.General.ControlCritico.Commands.UpdateControlCitico;
using Hsec.Application.General.ControlCritico.Models;
using Hsec.Application.General.ControlCritico.Queries.Code2Name;
using Hsec.Application.General.ControlCritico.Queries.GetControlCritico;
using Hsec.Application.General.ControlCritico.Queries.GetCriteriosByCC;
using Hsec.Application.General.ControlCritico.Queries.SearchControlCritico;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hsec.WebApi.Controllers.General
{    
    public class ControlCriticoController : ApiController
    {
        [HttpPost("Create")]
        public async Task<ActionResult<Unit>> Create(CreateControlCriticoCommand obj)
        {
            return await Mediator.Send(obj);
        }
        
        [HttpGet("{idCC}/Delete")]
        public async Task<ActionResult<Unit>> Delete(string idCC)
        {
            if (string.IsNullOrEmpty(idCC)) throw new ValidationException("idCC no valido");
            return await Mediator.Send(new DeleteControlCriticoCommand() { CodCC = idCC});
        }

        [HttpPost("{idCC}/Update")]
        public async Task<ActionResult<Unit>> Update(UpdateControlCriticoCommand obj,string idCC)
        {
            if(obj.data.CodCC != idCC && string.IsNullOrEmpty(idCC)) throw new ValidationException("idCC no valido");
            obj.CodCC = idCC;
            return await Mediator.Send(obj);
        }

        [HttpGet("{idCC}")]
        public async Task<ActionResult<ControlCriticoVM>> Get(string idCC)
        {
            if (string.IsNullOrEmpty(idCC)) throw new ValidationException("idCC no valido");
            return await Mediator.Send(new GetControlCriticoQuery() { CodCC = idCC });
        }

        [HttpGet("{idCC}/criterios")]
        public async Task<ActionResult<CriteriosVM>> GetCriterios(string idCC)
        {
            return await Mediator.Send(new GetCriteriosByCCQuery() { CodCC = idCC });
        }

        [HttpGet("Code2Name/CCC/{CodCC}/CCri/{CodCrit}")]
        public async Task<ActionResult<string>> GetCode2Name(string CodCC,string CodCrit)
        {
            return await Mediator.Send(new Code2NameQuery() { CodCC = CodCC, CodCrit = CodCrit });
        }

        [HttpPost("Buscar")]
        public async Task<ActionResult<SearchControlCriticoVM>> GetBuscar(SearchControlCriticoQuery obj)
        {
            return await Mediator.Send(obj);
        }

    }
}
