using Hsec.Application.General.Jerarquias.Commands.CreateJerarquias;
using Hsec.Application.General.Jerarquias.Commands.DeleteJerarquias;
using Hsec.Application.General.Jerarquias.Commands.UpdateJerarquias;
using Hsec.Application.General.Jerarquias.Queries.GetJerarquia;
using Hsec.Application.General.Jerarquias.Queries.Code2Name;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Hsec.WebApi.Controllers.General
{
    //[DisableCors]
    //[EnableCors("AnotherPolicy")]
    public class JerarquiaController : ApiController
    {
        
        [Route("Get/Jerarquia")]
        [HttpGet]
        public async Task<ActionResult<JerarquiaVM>> Get()
        {
            return await Mediator.Send(new GetJerarquiaQuery());
        }

        [Route("Code2Name/{codigo}")]
        [HttpGet]
        public async Task<ActionResult<string>> Get(string codigo)
        {
            return await Mediator.Send(new Code2NameQuery() { codigo = codigo });
        }

        [Route("Get/Jerarquia/All")]
        [HttpGet]
        public async Task<ActionResult<JerarquiaVM>> GetAll()
        {
            return await Mediator.Send(new GetJerarquiaAllQuery());
        }

        [Route("Get/Jerarquia/Gerencia/All")]
        [HttpGet]
        public async Task<ActionResult<JerarquiaVM>> GetGeAll()
        {
            return await Mediator.Send(new GetJerarquiaGerenciaAllQuery());
        }

        [Route("Get/Jerarquia/{CodPosicion}")]
        [HttpGet]
        public async Task<ActionResult<JerarquiaNodeVM>> GetId(int CodPosicion)
        {
            return await Mediator.Send(new GetJerarquiaIdQuery() { CodPosicion = CodPosicion });
        }

        [Route("JerarquiaNode/Create")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateJerarquiaNodeCommand command)
        {
            return await Mediator.Send(command);           
        }

        [Route("JerarquiaNode/Mover/{id}")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Mover(int id, MoverJerarquiaNodeCommand command)
        {        
            command.CodPosicion = id;                       
            return await Mediator.Send(command);
        }

        [Route("JerarquiaNode/Update/{id}")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Update(int id, UpdateJerarquiaNodeCommand command)
        {
            command.CodPosicion = id;            
            return await Mediator.Send(command);
        }

        [Route("JerarquiaNode/Delete/{id}")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            DeleteJerarquiaNodeCommand command = new DeleteJerarquiaNodeCommand();
            command.CodPosicion = id;            
            return await Mediator.Send(command);
        }

        [Route("Get/JerarquiaSupervisor/{codPosicion}")]
        [HttpGet]
        public async Task<ActionResult<List<SupervisorVM>>> GetJerarquiaSupervisor(int codPosicion)
        {
            return await Mediator.Send(new GetJerarquiaSupervisorQuery() { CodPosicion = codPosicion });
        }

    }
}