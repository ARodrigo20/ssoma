using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Personas.Queries.GetCode2NameOne
{

    public class GetCode2NameOneQuery : IRequest<string>
    {
        public string code { get; set; }

        public class GetCode2NameQueryOneHandler : IRequestHandler<GetCode2NameOneQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCode2NameQueryOneHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<string> Handle(GetCode2NameOneQuery request, CancellationToken cancellationToken)
            {
                var code = request.code;
                var person = _context.TPersona.Find(code);
                return (person != null) ? person.Nombres + ' ' + person.ApellidoPaterno + ' ' + person.ApellidoMaterno : null;
            }
        }
    }
}