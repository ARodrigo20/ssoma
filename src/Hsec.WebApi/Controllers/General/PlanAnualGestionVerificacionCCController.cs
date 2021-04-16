using System.Collections.Generic;
using System.Threading.Tasks;
using Hsec.Application.General.Empresa.Queries.GetCode2Name;
using Hsec.Application.General.Empresa.Queries.GetEmpresa;
using Hsec.Application.General.PlanAnualVerifiacionCC.Commands.UpdatePlanAnual;
using Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPerson;
using Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPersonFiltro;
using Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetModulo;
using Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetPlanAnual;
using Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetPlanAnualSeguimiento;
using Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetPlanAnualSeguimientoTotal;
using Microsoft.AspNetCore.Mvc;

namespace Hsec.WebApi.Controllers.General
{
    public class PlanAnualGestionVerificacionCCController : ApiController
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

        [HttpGet("Modelos")]
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

        [HttpGet("VerCC/Personas")]
        public async Task<ActionResult<VerCCPorPersonaVM>> GetVerCCPorPersonas(string CodPersona, string CodReferencia, string Anio, string CodMes)
        {
            return await Mediator.Send(new GetVerCCPorPersonasQuery(){CodPersona = CodPersona,CodReferencia = CodReferencia, Anio = Anio, CodMes = CodMes});
        }

        [HttpPost("VerCC/Personas/total")]
        public async Task<ActionResult<VerCCPorPersonaFiltroVM>> GetVerCCPorPersonasFiltro(FiltroVerCCPorPersonasFiltro filtro)
        {
            return await Mediator.Send(new GetVerCCPorPersonasFiltroQuery(){filtro = filtro});
        }

    }
}