using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Empresa.Queries.GetCode2Name
{

    public class GetCode2NameQuery : IRequest<string>
    {
        public string codigo { get; set; }

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
                //var resp = _context.TPersona
                //    .Where(t => data.Contains(t.CodPersona))
                //    .Select(t => t.Nombres + ' ' + t.ApellidoPaterno + ' ' + t.ApellidoMaterno)
                //    .ToList<string>();
                var res = _context.TProveedor.Find(request.codigo);

                return (res != null) ? res.RazonSocial : null;
            }
        }
    }
}