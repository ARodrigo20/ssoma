using System.Threading.Tasks;
using Hsec.Application.VerificacionObservacionCc.Queries.GetEjecutadosQuery;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Collections.Generic;

namespace Hsec.WebApi.Controllers.Observaciones
{
    public class VerificacionObservacionCcController : ApiController
    {
        //[HttpGet("getEjecutados/{codPersona}/{codReferencia}/{anio}/{codMes}")]
        [HttpGet("getEjecutados/{codPersona}/{anio}/{codMes}")]
        public async Task<ActionResult<EjecutadosVM>> GetEjecutados(string codPersona, string anio, string codMes)
        {
            return await Mediator.Send(new GetEjecutadosQuery() { CodPersona = codPersona, Anio = anio, CodMes = codMes });
        }

    }
}