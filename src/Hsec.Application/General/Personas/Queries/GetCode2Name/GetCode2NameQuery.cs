using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Personas.Queries.GetCode2Name
{

    public class GetCode2NameQuery : IRequest<List<string>>
    {
        public List<string> data { get; set; }

        public class GetCode2NameQueryHandler : IRequestHandler<GetCode2NameQuery, List<string>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCode2NameQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<string>> Handle(GetCode2NameQuery request, CancellationToken cancellationToken)
            {
                var data = request.data;
                var resp = _context.TPersona
                    .Where(t => data.Contains(t.CodPersona))
                    .Select(t => t.Nombres + ' ' + t.ApellidoPaterno + ' ' + t.ApellidoMaterno)
                    .ToList<string>();

                return resp;
            }
        }
    }
}