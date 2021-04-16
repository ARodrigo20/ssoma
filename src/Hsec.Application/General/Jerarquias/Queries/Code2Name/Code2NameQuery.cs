using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Jerarquias.Queries.Code2Name
{

    public class Code2NameQuery : IRequest<string>
    {
        //public int codigo { get; set; }
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
                if(request.codigo == null)
                {
                    return null;
                }
                else
                {
                    var resp = _context.TJerarquia.Find(Int32.Parse(request.codigo));
                    return (resp != null) ? resp.Descripcion : null;
                }
                
                //return _context.TJerarquia.Find(request.codigo).Descripcion;
            }
        }

    }
}