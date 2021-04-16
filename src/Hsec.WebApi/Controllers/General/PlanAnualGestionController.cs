using System.Collections.Generic;
using System.Threading.Tasks;
using Hsec.Application.General.Empresa.Queries.GetCode2Name;
using Hsec.Application.General.Empresa.Queries.GetEmpresa;
using Hsec.Application.General.PlanAnual.Commands.UpdatePlanAnual;
using Hsec.Application.General.PlanAnual.Models;
using Hsec.Application.General.PlanAnual.Queries.GetPlanAnual;
using Microsoft.AspNetCore.Mvc;
using Hsec.Application.General.PlanAnual.Queries.GetCartillas;
using Hsec.Application.General.PlanAnualCartillas.Queries.GetCartillasPorPerson;
using Hsec.Application.General.PlanAnualCartillas.Queries.GetPlanAnualSeguimiento;

namespace Hsec.WebApi.Controllers.General
{
    public class PlanAnualGestionController : ApiController
    {
        
        [HttpPost("Update")]
        public async Task<ActionResult> PostUpdate(UpdatePlanAnualVM obj)
        {
            await Mediator.Send(new UpdatePlanAnualCommand(){data = obj});
            return NoContent();
        }
        
        [HttpPost("Buscar")]
        public async Task<ActionResult<GetPlanAnualVM>> PostUpdate(FiltrosPlanAnual filtros)
        {
            return await Mediator.Send(new GetPlanAnualQuery(){filtros = filtros});
        }

        [HttpGet("Cartillas")]
        public async Task<ActionResult<GetCartillasVM>> GetCartillas()
        {
            return await Mediator.Send(new GetCartillasQuery(){});
        }
        
        [HttpGet("Cartillas/Personas")]
        public async Task<ActionResult<CartillaPorPersonaVM>> GetCartillasPorPersonas(string CodPersona, string Anio, string CodMes)
        {
            return await Mediator.Send(new GetCartillasPorPersonasQuery(){CodPersona = CodPersona, Anio = Anio, CodMes = CodMes});
        }

        [HttpPost("Seguimiento")]
        public async Task<ActionResult<GetPlanAnualSeguimientoVM>> GetSeguimiento(FiltrosPlanAnualSeguimiento filtros)
        {
            return await Mediator.Send(new GetPlanAnualSeguimientoQuery() { filtros = filtros });
        }
    }
}