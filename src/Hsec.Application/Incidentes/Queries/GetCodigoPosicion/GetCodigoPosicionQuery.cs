using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Incidentes.Queries.GetCodigoPosicion
{
    public class GetCodigoPosicionQuery : IRequest<IncidenteAprobacionVM>
    {
        public string codigo { get; set; }
        public class GetCodigoPosicionQueryHandler : IRequestHandler<GetCodigoPosicionQuery, IncidenteAprobacionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCodigoPosicionQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IncidenteAprobacionVM> Handle(GetCodigoPosicionQuery request, CancellationToken cancellationToken)
            {
                var vm = new IncidenteAprobacionVM();
                var incidente = _context.TIncidente.Where(t => t.CodIncidente.Equals(request.codigo)).FirstOrDefault();
                if (incidente == null) return vm;
                vm.Gerencia = incidente.CodPosicionGer;
                vm.SuperIntendencia = incidente.CodPosicionSup;
                vm.Tipo = incidente.CodAreaHsec;
                return vm;
            }
        }

    }
}