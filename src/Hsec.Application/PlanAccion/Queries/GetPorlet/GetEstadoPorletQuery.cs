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
    public class GetEstadoPorletQuery : IRequest<List<Tuple<string, int>>>
    {
        public string CodPersona { get; set; }
        public class GetEstadoPorletQueryHandler : IRequestHandler<GetEstadoPorletQuery, List<Tuple<string, int>>>
        {
            private readonly IApplicationDbContext _context;

            public GetEstadoPorletQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Tuple<string, int>>> Handle(GetEstadoPorletQuery request, CancellationToken cancellationToken)
            {
                //var data = (from ac in _context.TAccion.Where(i => i.Estado)
                //            join rp in _context.TResponsable.Where(i => i.Estado) on ac.CodAccion equals rp.CodAccion
                //            into lista
                //            from list in lista.DefaultIfEmpty()
                //            where (list.CodPersona == request.CodPersona)
                //            select new { ac.CodEstadoAccion, list.CodAccion }).ToList();
                var data = _context.TAccion.Include(t => t.RespPlanAccion)
                    .Where(a => (a.RespPlanAccion.Where(r => r.Estado).Select(r => r.CodPersona)).Contains(request.CodPersona) && a.Estado)
                    .Select(t => new { t.CodEstadoAccion, t.CodAccion }).ToList(); //t.RespPlanAccion.First().CodAccion

                return data.OrderByDescending(i => i.CodEstadoAccion).GroupBy(x => x.CodEstadoAccion).Select(y => new Tuple<string,int>(y.First().CodEstadoAccion, y.Count())).ToList();               
            }
        }
    }
}
