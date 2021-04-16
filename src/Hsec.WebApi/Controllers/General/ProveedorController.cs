using System.Threading.Tasks;
using Hsec.Application.General.Empresa.Queries.GetCode2Name;
using Hsec.Application.General.Empresa.Queries.GetEmpresa;
using Microsoft.AspNetCore.Mvc;

namespace Hsec.WebApi.Controllers.General
{
    public class ProveedorController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<ListEmpresaVM>> Get()
        {
            return await Mediator.Send(new GetEmpresaQuery());
        }
        [HttpGet("Code2Name/{idCode}")]
        public async Task<ActionResult<string>> Get(string idCode)
        {
            return await Mediator.Send(new GetCode2NameQuery() { codigo = idCode });
        }
    }
}
