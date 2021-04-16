using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.YoAseguro;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.YoAseguro.Queries.ExisteReunion
{
    public class ExisteReunionQuery : IRequest<bool>
    {
        public string Reunion { get; set; }
        public DateTime? FechaEvalucion { get; set; }

        public class ExisteReunionQueryHandler : IRequestHandler<ExisteReunionQuery, bool>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public ExisteReunionQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(ExisteReunionQuery request, CancellationToken cancellationToken)
            {
                var list = _context.TYoAseguro.Where(t => t.Estado == true 
                            && t.Reunion.Equals(request.Reunion) 
                            && t.FechaEvalucion.Value.Year == request.FechaEvalucion.Value.Year 
                            && t.FechaEvalucion.Value.Month == request.FechaEvalucion.Value.Month 
                            && t.FechaEvalucion.Value.Day == request.FechaEvalucion.Value.Day).ToList();

                return list.Count() > 0;
            }
        }
    }
}
