using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.AnalisisCausas.Queries.GetAnalisisCausaPartner
{
    public class GetAnalisisCausaPartnerQuery : IRequest<List<AnalisisCausaVM>>
    {
        public string CodPadre { get; set; }
        public class GetAnalisisCausaPartnerQueryHandler : IRequestHandler<GetAnalisisCausaPartnerQuery, List<AnalisisCausaVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAnalisisCausaPartnerQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AnalisisCausaVM>> Handle(GetAnalisisCausaPartnerQuery request, CancellationToken cancellationToken)
            {
                return _context.TAnalisisCausa.Where(a => a.CodPadre.Equals(request.CodPadre)).ProjectTo<AnalisisCausaVM>(_mapper.ConfigurationProvider).ToList();               
            }            
        }
    }
}
