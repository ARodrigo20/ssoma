using System;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Common.Interfaces;
using System.Collections.Generic;
using Hsec.Application.General.PlanAnualGeneral.Queries.GetModulosSeguimiento;

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetPorlet
{
    public class GetSeguimientoAnualQuery : IRequest<List<PlanAnualDto>>
    {
        public string CodPersona { get; set; }
        public class GetPlanesPorletQueryHandler : IRequestHandler<GetSeguimientoAnualQuery, List<PlanAnualDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            public GetPlanesPorletQueryHandler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<List<PlanAnualDto>> Handle(GetSeguimientoAnualQuery request, CancellationToken cancellationToken)
            {
                string Codpersona = request.CodPersona;
                string year = DateTime.Now.Year.ToString();
                string mes = DateTime.Now.Month.ToString();
                var planeados = _context.TPlanAnualGeneral.Where(p => p.Anio == year && p.CodMes == mes && p.CodPersona == request.CodPersona).Select(p=> new {p.CodReferencia,p.Valor});
                List<PlanAnualDto> Lista = new List<PlanAnualDto>();
                var modulos = _context.TModulo.Where(t => t.CodModuloPadre != null).Select(t => t.CodModulo);
              //  var Ejecutado = await _ejecutados.getPer("01.01", "0047318716", "2020", "11").ConfigureAwait(true);
                foreach (var item in modulos) {
                    //var ejecutados = await _ejecutados.getPer(item, Codpersona, year, mes).ConfigureAwait(true);
                    var ejecutados = await _mediator.Send(new GetModuloSeguimientoQuery() { Modulo = item, CodPersona = Codpersona, Anio = year, CodMes = mes });

                    var planeado = 0;
                    if(planeados.Any(p=>p.CodReferencia==item)) planeado=Convert.ToInt32(planeados.First(t => t.CodReferencia == item).Valor);
                    if (ejecutados > 0 || planeado > 0)
                    {
                        Lista.Add(new PlanAnualDto(item, ejecutados, planeado));
                    }
                }
                return Lista;
            }
        }
    }
}
