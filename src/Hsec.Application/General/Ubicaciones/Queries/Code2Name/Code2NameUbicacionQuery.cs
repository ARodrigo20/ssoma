using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Ubicaciones.Queries.Code2Name
{

    public class Code2NameUbicacionQuery : IRequest<string>
    {
        public string codigo { get; set; }
        public class Code2NameUbicacionQueryHandler : IRequestHandler<Code2NameUbicacionQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Code2NameUbicacionQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<string> Handle(Code2NameUbicacionQuery request, CancellationToken cancellationToken)
            {
                //return _context.TUbicacion.Find(request.codigo).Descripcion;
                var resp =  _context.TUbicacion.Find(request.codigo);
                return (resp != null) ? resp.Descripcion : null;
            }
        }

    }
}