using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Hsec.Application.General.Ubicaciones.Queries.GetUbicaciones;
using Hsec.Application.General.Ubicaciones.Commands.UpdateUbicaciones;
using Hsec.Application.General.Ubicaciones.Commands.CreateUbicaciones;
using Hsec.Application.General.Ubicaciones.Queries.Code2Name;

namespace Hsec.WebApi.Controllers.General
{    
    public class UbicacionesController : ApiController
    {
        [Route("Get")]
        [HttpGet]
        public async Task<ActionResult<UbicacionesVM>> Get()
        {
            return await Mediator.Send(new GetUbicacionesQuery());
        }

        [Route("Create")]
        [HttpPost]        
        public async Task<ActionResult<long>> Create(CreateUbicacionesCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("Update/{id}")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Update(int id, UpdateUbicacionesCommand command)
        {
            command.CodUbicacion = id.ToString();
            return await Mediator.Send(command);
        }

        [Route("Code2Name/{codigo}")]
        [HttpGet]
        public async Task<ActionResult<string>> Get(string codigo)
        {
            return await Mediator.Send(new Code2NameUbicacionQuery() { codigo = codigo });
        }
    }
}