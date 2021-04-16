using System.Threading.Tasks;
using Hsec.Application.General.Accesos.Queries.GetAccesos;
using Hsec.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hsec.WebApi.Controllers.General
{
    public class AccesoController : ApiController
    {
        //[HttpPost("Buscar")]
        //public async Task<ActionResult<GeneralCollection<AccesoVM>>> Get(GetAccesosQuery busqueda)
        //{
        //    return await Mediator.Send(busqueda);
        //}
        
        [HttpGet]
        public async Task<ActionResult<GeneralCollection<AccesoVM>>> Get()
        {
            return await Mediator.Send(new GetAccesosQuery()).ConfigureAwait(true);
        }

        [HttpGet("Get/{CodRol}")]
        public async Task<ActionResult<GeneralCollection<AccesoVM>>> Get(int CodRol)
        {
            return await Mediator.Send(new GetAccesosQuerybyRol() { CodRol = CodRol }).ConfigureAwait(true);
        }
    }
}