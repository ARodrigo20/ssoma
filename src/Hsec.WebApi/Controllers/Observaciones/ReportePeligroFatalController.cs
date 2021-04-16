using System.Threading.Tasks;
using Hsec.Application.ReportePeligroFatal.Queries.GetReportesQuery;
using Hsec.Application.ReportePeligroFatal.Queries.GetDesviacionQuery;
using Hsec.Application.ReportePeligroFatal.Queries.GetControlesQuery;
using Hsec.Application.ReportePeligroFatal.Commands.UpdateReporte;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Hsec.WebApi.Controllers.Observaciones
{
    public class ReportePeligroFatalController : ApiController
    {
        [HttpPost("buscar")]
        public async Task<ActionResult<ReportesVM>> Get(GetReportesQuery obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpPost("getDesviacion")]
        public async Task<ActionResult<DesviacionesVM>> GetDesviacion(GetDesviacionQuery obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpPost("getControles")]
        public async Task<ActionResult<ControlesVM>> GetControles(GetControlesQuery obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpPost("updateReporte")]
        public async Task<ActionResult<Unit>> GetControles(UpdateReporteCommand obj)
        {
            return await Mediator.Send(obj);
        }
    }
}