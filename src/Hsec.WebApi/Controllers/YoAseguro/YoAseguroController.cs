using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hsec.Application.YoAseguro.Commands.CreateYoAseguro;
using Hsec.Application.YoAseguro.Commands.UpdateYoAseguro;
using Hsec.Application.YoAseguro.Commands.DeleteYoAseguro;
using Hsec.Application.YoAseguro.Queries.SearchYoAseguro;
using Hsec.Application.YoAseguro.Queries.GetLugares;
using Hsec.Application.YoAseguro.Queries.GetYoAseguro;
using Hsec.Application.YoAseguro.Queries.ExisteReunion;
using Hsec.Application.YoAseguro.Queries.GetSeguimiento;
using Hsec.Application.YoAseguro.Models;
using MediatR;
using System;
using System.Collections.Generic;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hsec.WebApi.Controllers.YoAseguro
{
    public class YoAseguroController : ApiController
    {
        [HttpPost("Create")]
        public async Task<ActionResult<Unit>> Create(CreateYoAseguroCommand obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpPost("{codYoAseguro}/Update")]
        public async Task<ActionResult<Unit>> Update(UpdateYoAseguroCommand obj, string codYoAseguro)
        {
            if (obj.CodYoAseguro != codYoAseguro && string.IsNullOrEmpty(codYoAseguro)) throw new ValidationException("CodYoAseguro no valido");
            obj.CodYoAseguro = codYoAseguro;
            return await Mediator.Send(obj);
        }

        [HttpPost("Buscar")]
        public async Task<ActionResult<SearchYoAseguroVM>> GetBuscar(SearchYoAseguroQuery obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpGet("{cod}/Lugares")]
        public async Task<ActionResult<LugaresVM>> GetLugares(string cod)
        {
            return await Mediator.Send(new GetLugaresQuery() { Cod = cod });
        }

        [HttpGet("{codYoAseguro}")]
        public async Task<ActionResult<YoAseguroDto>> Get(string codYoAseguro)
        {
            if (string.IsNullOrEmpty(codYoAseguro)) throw new ValidationException("codYoAseguro no valido");
            return await Mediator.Send(new GetYoAseguroQuery() { CodYoAseguro = codYoAseguro });
        }

        [HttpPost("ExisteReunion")]
        public async Task<ActionResult<bool>> ExisteReunion(ExisteReunionQuery obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpGet("{codYoAseguro}/Delete")]
        public async Task<ActionResult<Unit>> Delete(string codYoAseguro)
        {

            if (string.IsNullOrEmpty(codYoAseguro)) throw new ValidationException("codYoAseguro no valido");
            return await Mediator.Send(new DeleteYoAseguroCommand() { CodYoAseguro = codYoAseguro });
        }

        [HttpGet("seguimiento/{id_modulo}/{id_persona}/{id_anio}/{id_mes}")]
        public async Task<ActionResult<int>> seguimiento(string id_modulo, string id_persona, int id_anio, int id_mes)
        {
            return await Mediator.Send(new GetSeguimientoQuery() { modulo = id_modulo, persona = id_persona, anio = id_anio, mes = id_mes });
        }

        [HttpPost("seguimiento")]
        public async Task<ActionResult<List<Tuple<string, int>>>> seguimiento(GetSeguimientosQuery seg)
        {
            return await Mediator.Send(seg);
        }
    }
}
