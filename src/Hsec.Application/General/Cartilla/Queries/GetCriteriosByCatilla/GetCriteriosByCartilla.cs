using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Cartilla.Queries.GetCriteriosByCatilla
{
    public class GetCriteriosByCartilla : IRequest<CriteriosByCartillaVM>
    {
        public string CodCartilla { get; set; }
        public class GetCriteriosByCartillaHandler : IRequestHandler<GetCriteriosByCartilla, CriteriosByCartillaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCriteriosByCartillaHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CriteriosByCartillaVM> Handle(GetCriteriosByCartilla request, CancellationToken cancellationToken)
            {
                var VM  = new CriteriosByCartillaVM();
                var ListCodCC = _context.TCartillaDetalle.Where(t => t.Estado == true && t.CodCartilla.Equals(request.CodCartilla)).Select(t => t.CodCC).ToList();
                
                var list = new List<CriterioDto>();

                foreach (var item in ListCodCC)
                {
                    var ControlCriticoItem = _context.TControlCritico.Find(item);
                    var listCriterios = _context.TCriterio.Where(t => t.Estado == true && t.CodCC.Equals(item))
                            .ProjectTo<CriterioDto>(_mapper.ConfigurationProvider)
                            .ToList();
                    listCriterios.ForEach(t => t.ControlCritico = ControlCriticoItem.DesCC);
                    list.AddRange(listCriterios);
                }
                VM.list = list;
                VM.size = list.Count;
                return VM;
            }
        }
    }
}