using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.ControlCritico.Queries.SearchControlCritico
{
    public class SearchControlCriticoQuery : IRequest<SearchControlCriticoVM>
    {
        public string CodCC { get; set; }
        public string ControlCritico { get; set; }
        public string PeligroFatal { get; set; }
        public string Riesgo { get; set; }
        public string TipoCC { get; set; }
        public string Modulo { get; set; }
        public class SearchControlCriticoQueryHandler : IRequestHandler<SearchControlCriticoQuery, SearchControlCriticoVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public SearchControlCriticoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SearchControlCriticoVM> Handle(SearchControlCriticoQuery request, CancellationToken cancellationToken)
            {
                SearchControlCriticoVM VM = new SearchControlCriticoVM();
                VM.list = _context.TControlCritico
                   .Where(    t => t.Estado == true
                           && (string.IsNullOrEmpty(request.CodCC) || t.CodCC.Equals(request.CodCC))
                           && (string.IsNullOrEmpty(request.PeligroFatal) || t.PeligroFatal.Equals(request.PeligroFatal))
                           && (string.IsNullOrEmpty(request.TipoCC) || t.TipoCC.Equals(request.TipoCC))
                           && (string.IsNullOrEmpty(request.ControlCritico) || t.DesCC.Contains(request.ControlCritico))
                           && (string.IsNullOrEmpty(request.Riesgo) || t.CodRiesgo.Contains(request.Riesgo))
                           && (string.IsNullOrEmpty(request.Modulo) || t.Modulo == request.Modulo))
                   .ProjectTo<SearchControlCriticoDto>(_mapper.ConfigurationProvider)
                   .ToList();

                VM.size = VM.list.Count;
                return VM;
            }
        }

    }
}