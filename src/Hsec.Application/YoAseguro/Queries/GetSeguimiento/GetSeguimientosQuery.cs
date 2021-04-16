using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Common.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System;

namespace Hsec.Application.YoAseguro.Queries.GetSeguimiento
{
    public class GetSeguimientosQuery : IRequest<List<Tuple<string, int>>>
    {
        public string modulo { get; set; }
        public List<string> personas { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }

        public class GetSeguimientosQueryHandler : IRequestHandler<GetSeguimientosQuery, List<Tuple<string, int>>>
        {
            private readonly IApplicationDbContext _context;

            public GetSeguimientosQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Tuple<string, int>>> Handle(GetSeguimientosQuery request, CancellationToken cancellationToken)
            {
                return _context.TYoAseguro
                    .Where(t => t.Estado && request.personas.Contains(t.CodPersonaResponsable) && t.FechaEvalucion.Value.Year == request.anio && t.FechaEvalucion.Value.Month == request.mes)
                    .GroupBy(t => t.CodPersonaResponsable).Select(g => new Tuple<string, int>(g.Key, g.Count())).ToList();
            }
        }
    }
}
