using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Comite.Queries.GetSeguimiento
{
    public class GetSeguimientoIndividualQuery : IRequest<int>
    {
        public string modulo { get; set; }
        public string persona { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }

        public class GetSeguimientoIndividualQueryHandler : IRequestHandler<GetSeguimientoIndividualQuery, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSeguimientoIndividualQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(GetSeguimientoIndividualQuery request, CancellationToken cancellationToken)
            {
                return _context.TComite
                    .Where(t => t.Fecha.Year == request.anio && t.Fecha.Month == request.mes && t.CreadoPor.Equals(request.persona))
                    .Count();
            }
        }
    }
}