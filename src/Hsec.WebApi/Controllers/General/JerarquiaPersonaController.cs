using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiaPersona;
using Hsec.Application.General.JerarquiasPersona.Commands.CreateJerarquiaPersona;
using Hsec.Application.General.JerarquiasPersona.Commands.AccionJerarquiaPersona;
using Hsec.Application.General.JerarquiasPersona.Commands.UpdateJerarquiasPersona;
using Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiasPersona;
using Hsec.Application.General.Jerarquias.Queries.GetJerarquia;
using Hsec.Application.Common.Models;
using Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiasPersonaAll;
using Hsec.Application.General.Jerarquias.Queries.GetJerarquiasPersonaAll;

namespace Hsec.WebApi.Controllers.General
{
    
    public class JerarquiaPersonaController : ApiController
    {
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ActionResult<JerarquiasPersonaVM>> Get(int id)
        {
            var valor = await Mediator.Send(new GetJerarquiasPersonaQuery() { CodPosicion = id });
            return valor;
        }
        [Route("GetAll/{id}")]
        [HttpGet]
        public async Task<ActionResult<GeneralCollection<PersonaPosVM>>> GetAll(int id)
        {
            return await Mediator.Send(new GetJerarquiasPersonaAllQuery() { CodPosicion = id }).ConfigureAwait(true);
            
        }
        
        [Route("Create")]
        [HttpPost]       
        public async Task<ActionResult<Unit>> Create(CreateJerarquiaPersonaCommand command)
        {
            return await Mediator.Send(command);
        }
        [Route("Get/JerarquiaPersonaGS/{codPersona}")]
        [HttpGet]
        public async Task<JerarquiaVM> GetJerarquiaPersonaGS(string codPersona)
        {
            return await Mediator.Send(new JerarquiaPersonaGSQuery() { 
                codPersona = codPersona    
            });
        }
        [Route("Get/JerarquiaPersonaGS2/{codPersona}")]
        [HttpGet]
        public async Task<JerarquiaVM> GetJerarquiaPersonaGS2(string codPersona)
        {
            return await Mediator.Send(new JerarquiaPersonaGS2Query() { 
                codPersona = codPersona    
            });
        }
        [Route("Update")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Update(UpdateJerarquiaPersonaCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("AccionJerarquiaPersona")]
        [HttpPost]
        public async Task<AccionJerarquiaPersonaVM> GetAccionJerarquiaPersona(AccionJerarquiaPersonaVM modelVM)
        {
            return await Mediator.Send(new AccionJerarquiaPersonaCommand() { modelVM = modelVM });
        }
    }
}