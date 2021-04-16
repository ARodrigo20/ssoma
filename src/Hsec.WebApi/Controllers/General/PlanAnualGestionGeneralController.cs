using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Hsec.Application.General.PlanAnualGeneral.Commands.UpdatePlanAnualGeneral;
using Hsec.Application.General.PlanAnualGeneral.Queries.GetModulo;
using Hsec.Application.General.PlanAnualGeneral.Queries.GetPlanAnualGeneral;
using Hsec.Application.General.PlanAnualGeneral.Queries.GetPlanAnualSeguimiento;
using Hsec.Application.General.PlanAnualGeneral.Queries.GetPlanAnualSeguimientoTotal;
using Hsec.Application.General.PlanAnualGeneral.Queries.GetPorlet;
using Microsoft.AspNetCore.Mvc;

namespace Hsec.WebApi.Controllers.General
{
    public class PlanAnualGeneralGestionGeneralController : ApiController
    {
        
        [HttpPost("Update")]
        public async Task<ActionResult> PostUpdate(UpdatePlanAnualGeneralVM obj)
        {
            await Mediator.Send(new UpdatePlanAnualGeneralCommand(){data = obj});
            return NoContent();
        }
        
        [HttpPost("Buscar")]
        public async Task<ActionResult<GetPlanAnualGeneralVM>> PostUpdate(FiltrosPlanAnualGeneral filtros)
        {
            return await Mediator.Send(new GetPlanAnualGeneralQuery(){filtros = filtros});
        }

        [HttpGet("Modulos")]
        public async Task<ActionResult<GetModulosVM>> GetModulo()
        {
            return await Mediator.Send(new GetModuloQuery(){});
        }

        [HttpPost("Seguimiento")]
        public async Task<ActionResult<GetPlanAnualSeguimientoVM>> GetSeguimiento(FiltrosPlanAnualGeneralSeguimiento filtros)
        {
            return await Mediator.Send(new GetPlanAnualSeguimientoQuery(){filtros = filtros});
        }

        [HttpPost("Seguimiento/total")]
        public async Task<ActionResult<GetPlanAnualSeguimientoTotalVM>> GetSeguimientoTotal(FiltrosPlanAnualGeneralSeguimientoTotal filtros)
        {
            return await Mediator.Send(new GetPlanAnualSeguimientoTotalQuery(){filtros = filtros});
        }
        
        [HttpGet("portlet/getPlanAnual")]
        public async Task<List<PlanAnualDto>> GetPlanAnual()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var codPersona = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;
            return await Mediator.Send(new GetSeguimientoAnualQuery() { CodPersona= codPersona });
        }   
    }

}