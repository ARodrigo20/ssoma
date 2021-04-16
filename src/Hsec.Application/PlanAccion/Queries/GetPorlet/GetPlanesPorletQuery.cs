using System;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Common.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hsec.Application.PlanAccion.Queries.GetPorlet
{
    public class GetPlanesPorletQuery : IRequest<List<PlanesDto>>
    {
        public string CodPersona { get; set; }
        public class GetPlanesPorletQueryHandler : IRequestHandler<GetPlanesPorletQuery, List<PlanesDto>>
        {
            private readonly IApplicationDbContext _context;

            public GetPlanesPorletQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<PlanesDto>> Handle(GetPlanesPorletQuery request, CancellationToken cancellationToken)
            {
                //var data = (from ac in _context.TAccion.Where(i => i.Estado)
                //            join rp in _context.TResponsable.Where(i => i.Estado) on ac.CodAccion equals rp.CodAccion
                //            into lista
                //            from list in lista.DefaultIfEmpty()
                //            where (list.CodPersona == request.CodPersona)
                //            select new { ac.CodEstadoAccion, list.CodAccion }).ToList();
                return _context.TAccion.Include(t => t.RespPlanAccion)
                    .Where(a => (a.RespPlanAccion.Where(r => r.Estado).Select(r => r.CodPersona)).Contains(request.CodPersona) && a.Estado)
                    .Select(t => new PlanesDto { 
                        CodAccion=t.CodAccion,
                        DocReferencia=t.DocReferencia,
                        Tarea=t.Tarea,
                        TablaRef= t.CodTablaRef,
                        FechaFinal =t.FechaFinal,
                        Estado=t.CodEstadoAccion
                    }).ToList();              
            }
        }
    }
}
