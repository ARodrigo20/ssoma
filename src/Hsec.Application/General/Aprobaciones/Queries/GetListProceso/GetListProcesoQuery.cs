using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Queries.GetListProceso
{
    public class GetListProcesoQuery : IRequest<ListaProcesoVM>
    {
        public class GetListProcesoQueryHandler : IRequestHandler<GetListProcesoQuery, ListaProcesoVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetListProcesoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ListaProcesoVM> Handle(GetListProcesoQuery request, CancellationToken cancellationToken)
            {
                var list = _context.TProceso
                    .Where(t => t.Estado==true)
                    .ProjectTo<ProcesoDto>(_mapper.ConfigurationProvider)
                    .ToList();
                
                var vm = new ListaProcesoVM();
                vm.Lista = list;
                return vm;
            }
        }
    }
}