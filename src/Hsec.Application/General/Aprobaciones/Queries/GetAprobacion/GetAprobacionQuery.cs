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

namespace Hsec.Application.General.Aprobaciones.Queries.GetAprobacion
{
    public class GetAprobacionQuery : IRequest<GetAprobacionVM>
    {
        public int Correlativo { get; set; }
        public string DocReferencia { get; set; }
        public class GetAprobacionQueryHandler : IRequestHandler<GetAprobacionQuery, GetAprobacionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAprobacionQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetAprobacionVM> Handle(GetAprobacionQuery request, CancellationToken cancellationToken)
            {
                var res = _context.TAprobacion
                    .Where(t => t.DocReferencia.Equals(request.DocReferencia) && t.Estado == true)
                    .OrderByDescending(t => t.Creado)
                    .ProjectTo<GetAprobacionVM>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();
                return res;
            }
        }
    }
}