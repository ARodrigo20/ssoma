using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Common.Interfaces;
using AutoMapper;

namespace Hsec.Application.YoAseguro.Queries.GetSeguimiento
{
    public class GetSeguimientoQuery : IRequest<int>
    {
        public string modulo { get; set; }
        public string persona { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }

        public class GetSeguimientoQueryHandler : IRequestHandler<GetSeguimientoQuery, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSeguimientoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(GetSeguimientoQuery request, CancellationToken cancellationToken)
            {
                return _context.TYoAseguro
                    .Where(t => t.Estado && t.CodPersonaResponsable == request.persona && t.FechaEvalucion.Value.Year == request.anio && t.FechaEvalucion.Value.Month == request.mes)
                    .Count();
            }
        }
    }
}
