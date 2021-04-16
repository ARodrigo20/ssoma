using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Hsec.Application.General.Maestro.Commands.CreateMaestroData;
using Hsec.Application.General.Maestro.Commands.DeleteMaestroData;
using Hsec.Application.General.Maestro.Commands.UpdateMaestroData;
using System.Collections.Generic;
using Hsec.Application.General.Maestro.Queries.GetMaestroDescripcion;
using Hsec.Application.General.Maestro.Queries.GetCode2Name;
using Hsec.Application.General.Maestro.Queries.GetTablaQuery;
using Hsec.Application.General.Maestro.Queries.GetMaestroData;

namespace Hsec.WebApi.Controllers.General
{  
    public class MaestroController : ApiController
    {
        [HttpPost("create")]
        public async Task<ActionResult<int>> GetMaestroData(CreateMaestroDataCommand obj)
        {
            //obj.CodTabla = idTabla;
            return await Mediator.Send(obj);
        }

        [HttpGet("{idTabla}/delete/{idMaestro}")]
        public async Task<ActionResult<int>> DeleteMaestroData(string idMaestro, string idTabla)
        {
            if (idMaestro == null || idTabla == null)
            {
                return new BadRequestResult();
            }
            await Mediator.Send(new DeleteMaestroDataCommand() { CodMaestro = idMaestro, CodTabla = idTabla });
            return new NoContentResult();
        }

        [HttpPost("{idTabla}/update/{idMaestro}")]
        public async Task<ActionResult<int>> UpdateMaestroData(UpdateMaestroDataCommand obj, string idMaestro, string idTabla)
        {
            if (idMaestro == null || idTabla == null)
            {
                return new BadRequestResult();
            }
            obj.CodTabla = idTabla;
            obj.CodMaestro = idMaestro;
            return await Mediator.Send(obj);
        }

        [HttpGet]
        public async Task<IList<MaestroDataVM>> Get()
        {
            return await Mediator.Send(new GetMaestroDataQuery());
        }

        [HttpGet("tabla/{idTabla}/codigo/{idMaestro}")]
        public async Task<ActionResult<string>> Get(string idMaestro, string idTabla)
        {
            return await Mediator.Send(new GetCode2NameQuery() { CodMaestro=idMaestro,CodTable=idTabla });
        }

        [HttpPost("descripcion")]
        public async Task<ActionResult<ICollection<string>>> GetDescripcion(ICollection<string> data)
        {
            var a =  await Mediator.Send(new GetMaestroDescripcionQuery() { data = data });
            return new List<string>();
        }

        [HttpGet("tabla/{codTabla}")]
        public async Task<TablaVM> Get(string codTabla)
        {
            return await Mediator.Send(new GetTablaQuery() { CodTabla = codTabla });
        }

    }
}