using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.General.Maestro.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Maestro.Queries.GetTablaQuery
{
    public class GetTablaQuery : IRequest<TablaVM>
    {
        public string CodTabla { get; set; }
        public class GetTablaQueryHandler : IRequestHandler<GetTablaQuery, TablaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetTablaQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<TablaVM> Handle(GetTablaQuery request, CancellationToken cancellationToken)
            {
                TablaVM VM = new TablaVM();
                VM.data = _context.TMaestro
                    .Where(t => t.Estado == true && t.CodTabla.Equals(request.CodTabla))
                    .ProjectTo<MaestroDto>(_mapper.ConfigurationProvider)
                    .ToHashSet();

                VM.size = VM.data.Count();

                return VM;
            }

        }
    }
}