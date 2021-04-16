using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.ControlCritico.Queries.Code2Name
{

    public class Code2NameQuery : IRequest<string>
    {
        public string CodCrit { get; set; }
        public string CodCC { get; set; }
        public class Code2NameQueryHandler : IRequestHandler<Code2NameQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Code2NameQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<string> Handle(Code2NameQuery request, CancellationToken cancellationToken)
            {
                var data = _context.TCriterio
                    .Where(
                        t => t.Estado == true && 
                        t.CodCrit.Equals(request.CodCrit) &&
                        t.CodCC.Equals(request.CodCC))
                    .FirstOrDefault();
                if(data == null) return "";
                else return data.Criterio;
            }
        }

    }
}