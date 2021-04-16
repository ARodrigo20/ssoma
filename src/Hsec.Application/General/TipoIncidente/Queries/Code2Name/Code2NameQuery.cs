using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.TipoIncidente.Queries.Code2Name
{

    public class Code2NameTipoIncidenteQuery : IRequest<string>
    {
        public string codigo { get; set; }
        public class Code2NameTipoIncidenteQueryHandler : IRequestHandler<Code2NameTipoIncidenteQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Code2NameTipoIncidenteQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<string> Handle(Code2NameTipoIncidenteQuery request, CancellationToken cancellationToken)
            {
                var res = _context.TTipoIncidente.Find(request.codigo);
                return (res != null) ? res.Descripcion : null;
            }
        }

    }
}