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

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetModulo
{
    public class GetModuloQuery : IRequest<GetModulosVM>
    {
        public class GetModuloQueryHandler : IRequestHandler<GetModuloQuery, GetModulosVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetModuloQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetModulosVM> Handle(GetModuloQuery request, CancellationToken cancellationToken)
            {
                var vm = new GetModulosVM();
                vm.list = _context.TModulo.Where(t => t.Estado==true).Select(t => t.CodModulo).ToList();
                return vm;
            }
        }
    }
}