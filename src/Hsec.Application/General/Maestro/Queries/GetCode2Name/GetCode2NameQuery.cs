using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Maestro.Queries.GetCode2Name
{
    public class GetCode2NameQuery : IRequest<string>
    {
        public string CodTable { get; set; }
        public string CodMaestro { get; set; }
        public class GetCode2NameQueryHandler : IRequestHandler<GetCode2NameQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCode2NameQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<string> Handle(GetCode2NameQuery request, CancellationToken cancellationToken)
            {
                var desc = _context.TMaestro.Find(request.CodMaestro, request.CodTable);
                return (desc != null) ? desc.Descripcion : null;
                //return _context.TMaestro.Find(request.CodMaestro, request.CodTable).Descripcion;
            }

        }
    }
}
