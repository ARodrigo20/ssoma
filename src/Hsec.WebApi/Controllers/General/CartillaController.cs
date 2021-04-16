using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Hsec.Application.General.Cartilla.Commands.CreateCartilla;
using Hsec.Application.General.Cartilla.Commands.DeleteCartilla;
using Hsec.Application.General.Cartilla.Commands.UpdateCartilla;
using Hsec.Application.General.Cartilla.Models;
using Hsec.Application.General.Cartilla.Queries.GetCartilla;
using Hsec.Application.General.Cartilla.Queries.GetCriteriosByCatilla;
using Hsec.Application.General.Cartilla.Queries.SearchCartilla;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hsec.WebApi.Controllers.General
{    
    public class CartillaController : ApiController
    {
        [HttpPost("Create")]
        public async Task<ActionResult<Unit>> Create(CreateCartillaCommand obj)
        {
            return await Mediator.Send(obj);
        }
        
        [HttpGet("{idCartilla}/Delete")]
        public async Task<ActionResult<Unit>> Delete(string idCartilla)
        {
            if (string.IsNullOrEmpty(idCartilla)) throw new ValidationException("idCartilla no valido");
            return await Mediator.Send(new DeleteCartillaCommand() { CodCartilla = idCartilla });
        }

        [HttpPost("{idCartilla}/Update")]
        public async Task<ActionResult<Unit>> Update(UpdateCartillaCommand obj,string idCartilla)
        {
            if(obj.data.CodCartilla != idCartilla && string.IsNullOrEmpty(idCartilla)) throw new ValidationException("idCartilla no valido");
            obj.CodCartilla = idCartilla;
            return await Mediator.Send(obj);
        }

        [HttpGet("{idCartilla}")]
        public async Task<ActionResult<CartillaDto>> Get(string idCartilla)
        {
            if (string.IsNullOrEmpty(idCartilla)) throw new ValidationException("idCartilla no valido");
            return await Mediator.Send(new GetCartillaQuery() { CodCartilla = idCartilla });
        }

        [HttpGet("{idCartilla}/Criterios")]
        public async Task<ActionResult<CriteriosByCartillaVM>> GetCriteriosByCartilla(string idCartilla)
        {
            return await Mediator.Send(new GetCriteriosByCartilla(){CodCartilla = idCartilla });
        }

        [HttpPost("Buscar")]
        public async Task<ActionResult<SearchCartillaVM>> GetBuscar(SearchCartillaQuery obj)
        {
            return await Mediator.Send(obj);
        }
        [HttpPost("Buscar/Covid")]
        public async Task<ActionResult<SearchCartillaVM>> GetBuscarCovid(SearchCartillaCovidQuery obj)
        {
            return await Mediator.Send(obj);
        }
    }
}
