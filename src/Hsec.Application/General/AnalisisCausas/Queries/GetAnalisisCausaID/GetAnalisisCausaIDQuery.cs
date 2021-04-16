using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.General.AnalisisCausas.Queries.GetAnalisisCausas;

namespace Hsec.Application.General.AnalisisCausas.Queries.GetAnalisisCausaID
{
    public class GetAnalisisCausaIDQuery : IRequest<AnalisisCausaDto>
    {
        public string CodAnalisis { get; set; }
        public class GetAnalisisCausaIDQueryHandler : IRequestHandler<GetAnalisisCausaIDQuery, AnalisisCausaDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAnalisisCausaIDQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AnalisisCausaDto> Handle(GetAnalisisCausaIDQuery request, CancellationToken cancellationToken)
            {

                var AnalisisCausas = _context.TAnalisisCausa.Where(i => i.Estado).Include(i => i.Hijos).AsQueryable().ToList();
                int cont = 0;
                foreach (var item in AnalisisCausas)
                {
                    cont += item.Hijos.Count();
                }                
                if (!AnalisisCausas.Any(a=>a.CodAnalisis == request.CodAnalisis)) throw new GeneralFailureException("No existe Codigo en Base de datos");
                
                return recursion(AnalisisCausas.Find(i=>i.CodAnalisis == request.CodAnalisis));
            }
            public AnalisisCausaDto recursion(TAnalisisCausa it)
            {
                var AnalisisCausa = _mapper.Map<AnalisisCausaDto>(it);

                if (it.Hijos.Count == 0)
                {
                    return AnalisisCausa;
                }

                if (it.Hijos.Count > 0)
                {
                    foreach (var hijo in it.Hijos)
                    {
                        if (hijo.Estado) AnalisisCausa.children.Add(recursion(hijo));
                    }
                }

                return AnalisisCausa;
            }
        }
    }
}
