using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Incidentes.Models;
using Hsec.Domain.Entities.Incidentes;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteICAM
{
    
    public class GetIncidenteICAMQuery : IRequest<List<ICAMDto>> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteICAMQueryHandler : IRequestHandler<GetIncidenteICAMQuery, List<ICAMDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIncidenteICAMQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ICAMDto>> Handle(GetIncidenteICAMQuery request, CancellationToken cancellationToken)
            {
                var VM = new List<ICAMDto>();
                var CODIGO_INCIDENTE = request.CodIncidente;

                TIncidente objTIncidente = _context.TIncidente.Find(request.CodIncidente);

                if (objTIncidente == null || objTIncidente.Estado == false) throw new NotFoundException("Incidente",request.CodIncidente);

                VM = _context.TIcam
                    .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                    .ProjectTo<ICAMDto>(_mapper.ConfigurationProvider)
                    .ToList();

                return VM;
            }
        }

    }
}