using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.General.ControlCritico.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.ControlCritico.Queries.GetCriteriosByCC
{
    public class GetCriteriosByCCQuery : IRequest<CriteriosVM>
    {
        public string CodCC { get; set; }
        public class GetCriteriosByCCQueryHandler : IRequestHandler<GetCriteriosByCCQuery, CriteriosVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCriteriosByCCQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CriteriosVM> Handle(GetCriteriosByCCQuery request, CancellationToken cancellationToken)
            {
                CriteriosVM VM = new CriteriosVM();
                VM.list = _context.TCriterio
                    .Where(t => t.Estado == true && t.CodCC.Equals(request.CodCC))
                    .ProjectTo<CriterioDto>(_mapper.ConfigurationProvider)
                    .ToHashSet();

                VM.size = VM.list.Count();

                return VM;
            }
        }

    }
}