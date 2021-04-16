using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.GestionRiesgo.Queries.Code2Name
{

    public class Code2DetalleAsociadoQuery : IRequest<string>
    {
        public string codigo { get; set; }
        public class Code2DetalleAsociadoQueryHandler : IRequestHandler<Code2DetalleAsociadoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Code2DetalleAsociadoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<string> Handle(Code2DetalleAsociadoQuery request, CancellationToken cancellationToken)
            {
                return _context.TGestionRiesgo.Find(request.codigo).DetalleAsociado;
            }
        }

    }
}