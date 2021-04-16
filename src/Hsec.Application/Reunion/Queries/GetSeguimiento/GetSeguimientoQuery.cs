using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Reunion.Queries.GetSeguimiento
{
    public class GetSeguimientoQuery : IRequest<List<Tuple<string, int>>>
    {
        public string persona;

        public string modulo { get; set; }
        public List<string> personas { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }

        public class GetSeguimientoQueryHandler : IRequestHandler<GetSeguimientoQuery, List<Tuple<string, int>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSeguimientoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<Tuple<string, int>>> Handle(GetSeguimientoQuery request, CancellationToken cancellationToken)
            {
                return _context.TReunion
                    .Where(t => t.Estado && request.personas.Contains(t.CodPerFacilitador) && t.Fecha.Year == request.anio && t.Fecha.Month == request.mes)
                    .GroupBy(t => t.CodPerFacilitador).Select(g => new Tuple<string, int>(g.Key, g.Count())).ToList();;
            }
        }
    }
}
