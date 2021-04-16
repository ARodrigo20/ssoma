using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetAll.DTOs;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetAll.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetAll
{
    public class GetPlanTemaAllQuery : IRequest<GetPlanTemaAllVM>
    {
        public class GetFiltradoQueryHandler : IRequestHandler<GetPlanTemaAllQuery, GetPlanTemaAllVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetFiltradoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<GetPlanTemaAllVM> Handle(GetPlanTemaAllQuery request, CancellationToken cancellationToken)
            {
                var planTemas = _context.TPlanTema.AsQueryable().Where(i => i.Estado);
                GetPlanTemaAllVM modelVM = new GetPlanTemaAllVM();

                IList<GetPlanTemaAllDto> objeto = planTemas
                .ProjectTo<GetPlanTemaAllDto>(_mapper.ConfigurationProvider).OrderByDescending(i => i.codTemaCapacita)
                .ToList();

                modelVM.data = objeto;
                modelVM.count = objeto.Count();

                return modelVM;
            }
        }
    }
}
