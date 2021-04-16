using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.GestionRiesgo.Queries.Code2Name
{

    public class Code2NameQuery : IRequest<string>
    {
        public string codigo { get; set; }
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
                return _context.TGestionRiesgo.Find(request.codigo).Descripcion;
            }
        }

    }
}