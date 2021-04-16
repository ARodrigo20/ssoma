using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Hsec.Application.General.Modulos.Queries.GetModulo;
using Hsec.Application.General.Modulos.Commands.UpdateModulo;
using Hsec.Application.General.Modulos.Commands.CreateModulo;
using Hsec.Application.General.Modulos.Queries.Code2Name;

namespace Hsec.WebApi.Controllers.General
{    
    public class ModulosController : ApiController
    {
        [Route("Get")]
        [HttpGet]
        public async Task<ActionResult<ModuloVM>> Get()
        {
            return await Mediator.Send(new GetModuloQuery());
        }

        [Route("Create")]
        [HttpPost]        
        public async Task<ActionResult<long>> Create(CreateModuloCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("Update/{id}")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Update(int id, UpdateModuloCommand command)
        {
            command.CodModulo = id.ToString();
            return await Mediator.Send(command);
        }

        [Route("Code2Name/{codigo}")]
        [HttpGet]
        public async Task<ActionResult<string>> Get(string codigo)
        {
            return await Mediator.Send(new Code2NameQuery() { codigo = codigo });
        }
    }
}