using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.PlanAnual.Queries.GetCartillas
{
    public class GetCartillasQuery : IRequest<GetCartillasVM>
    {
        public class GetCartillasQueryHandler : IRequestHandler<GetCartillasQuery, GetCartillasVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCartillasQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetCartillasVM> Handle(GetCartillasQuery request, CancellationToken cancellationToken)
            {
                var vm = new GetCartillasVM();
                vm.list = _context.TCartilla.Where(t => t.Estado==true).Select(t => t.CodCartilla).ToList();
                return vm;
            }
        }
    }
}