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

namespace Hsec.Application.Auditoria.Queries.GetSeguimientoAuditoria
{
    public class GetSeguimientoAuditoria : IRequest<int>
    {
        public string 
        modulo { get; set; }
        public string persona { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }
        public class GetSeguimientoAuditoriaHandler : IRequestHandler<GetSeguimientoAuditoria, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSeguimientoAuditoriaHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(GetSeguimientoAuditoria request, CancellationToken cancellationToken)
            {
                return _context.TAuditoria.Where(
                        t => t.Estado == true 
                        && t.CodRespAuditoria.Equals(request.persona) 
                        && t.FechaFin.Value.Year.Equals(request.anio)
                        && t.FechaFin.Value.Month.Equals(request.mes)
                        )
                        .Count();
            }
        }
    }
}