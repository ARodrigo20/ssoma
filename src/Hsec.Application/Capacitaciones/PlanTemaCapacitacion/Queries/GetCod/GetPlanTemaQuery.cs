using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetCod.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetCod
{
    public class GetPlanTemaQuery : IRequest<GetPlanTemaVM>
    {
        public string codReferencia { get; set; }
        public string codTemaCapacita { get; set; }
        public class GetPlanTemaQueryHandler : IRequestHandler<GetPlanTemaQuery, GetPlanTemaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPlanTemaQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetPlanTemaVM> Handle(GetPlanTemaQuery request, CancellationToken cancellationToken)
            {
                var planTemas = _context.TPlanTema.Where(i => i.CodReferencia == request.codReferencia && i.CodTemaCapacita == request.codTemaCapacita && i.Estado).AsQueryable();
                GetPlanTemaVM modelVM = new GetPlanTemaVM();
                modelVM = planTemas
                .ProjectTo<GetPlanTemaVM>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

                return modelVM;
            }
        }


    }
}
