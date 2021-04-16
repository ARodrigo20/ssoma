using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Hsec.Application.General.TipoIncidente.Queries.GetTipoIncidente;
using Hsec.Application.General.TipoIncidente.Queries.Code2Name;
using Hsec.Application.General.TipoIncidente.Commands.CreateTipoIncidente;
using Hsec.Application.General.TipoIncidente.Commands.UpdateTipoIncidente;

namespace Hsec.WebApi.Controllers.General
{    
    public class TipoIncidenteController : ApiController
    {
        [Route("Get")]
        [HttpGet]
        public async Task<ActionResult<TipoIncidenteVM>> Get()
        {
            return await Mediator.Send(new GetTipoIncidenteQuery());
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateTipoIncidenteCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("Update/{id}")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Update(int id, UpdateTipoIncidenteCommand command)
        {
            command.CodTipoIncidente = id.ToString();
            return await Mediator.Send(command);
        }

        [Route("Code2Name/{codigo}")]
        [HttpGet]
        public async Task<ActionResult<string>> Get(string codigo)
        {
            return await Mediator.Send(new Code2NameTipoIncidenteQuery() { codigo = codigo });
        }
    }
}