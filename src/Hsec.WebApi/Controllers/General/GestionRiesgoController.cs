using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Hsec.Application.General.GestionRiesgo.Queries.GetGestionRiesgo;
using Hsec.Application.General.GestionRiesgo.Commands.CreateGestionRiesgo;
using Hsec.Application.General.GestionRiesgo.Commands.UpdateGestionRiesgo;
using Hsec.Application.General.GestionRiesgo.Queries.Code2Name;

namespace Hsec.WebApi.Controllers.General
{    
    public class GestionRiesgoController : ApiController
    {
        [Route("Get")]
        [HttpGet]
        public async Task<ActionResult<GestionRiesgoVM>> Get()
        {
            return await Mediator.Send(new GetGestionRiesgoQuery());
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateGestionRiesgoCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("Update/{id}")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Update(int id, UpdateGestionRiesgoCommand command)
        {
            command.CodGestionRiesgo = id.ToString();
            return await Mediator.Send(command);
        }

        [Route("Code2Descripcion/{codigo}")]
        [HttpGet]
        public async Task<ActionResult<string>> Get(string codigo)
        {
            return await Mediator.Send(new Code2NameQuery() { codigo = codigo });
        }
        
        [Route("Code2DetalleAsociado/{codigo}")]
        [HttpGet]
        public async Task<ActionResult<string>> GetDetalleAsociado(string codigo)
        {
            return await Mediator.Send(new Code2DetalleAsociadoQuery() { codigo = codigo });
        }
    }
}