using AutoMapper;
using Hsec.Application.Common.Models;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.AnalisisCausas.Queries.GetAnalisisCausas
{
    public class GetAnalisisCausasAllQuery : IRequest<GeneralCollection<AnalisisCausaDto>>
    {
        public class GetAnalisisCausasQueryHandler : IRequestHandler<GetAnalisisCausasAllQuery, GeneralCollection<AnalisisCausaDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAnalisisCausasQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GeneralCollection<AnalisisCausaDto>> Handle(GetAnalisisCausasAllQuery request, CancellationToken cancellationToken)
            {               
                var AnalisisCausa = _context.TAnalisisCausa.Where(i => i.Estado).Include(i => i.Hijos).AsQueryable();
                int cont = 0;
                foreach (var Jerarq in AnalisisCausa)
                {
                    cont += Jerarq.Hijos.Count();
                }
                var AllAnalisisCausa = new List<AnalisisCausaDto>();
                foreach (var item in AnalisisCausa.Where(i => i.CodPadre == null)) 
                {
                    AllAnalisisCausa.Add(recursion(item));
                }
                return new GeneralCollection<AnalisisCausaDto>(AllAnalisisCausa, cont);              
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

                        if (hijo.Estado)
                        {
                            AnalisisCausa.children.Add(recursion(hijo));
                        }
                    }
                }

                return AnalisisCausa;
            }
        }
    }
}
