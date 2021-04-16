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

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAnalisisCausalidad
{

    public class GetIncidenteAnalisisCausalidadQuery : IRequest<List<AnalisisCausalidadDto>>
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAnalisisCausalidadQueryHandler : IRequestHandler<GetIncidenteAnalisisCausalidadQuery, List<AnalisisCausalidadDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIncidenteAnalisisCausalidadQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AnalisisCausalidadDto>> Handle(GetIncidenteAnalisisCausalidadQuery request, CancellationToken cancellationToken)
            {
                var VM = new List<AnalisisCausalidadDto>();
                var CODIGO_INCIDENTE = request.CodIncidente;

                TIncidente objTIncidente = _context.TIncidente.Find(request.CodIncidente);

                if (objTIncidente == null || objTIncidente.Estado == false) throw new NotFoundException("Incidente", request.CodIncidente);

                VM = _context.TIncidenteAnalisisCausa
                    .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                    .ProjectTo<AnalisisCausalidadDto>(_mapper.ConfigurationProvider)
                    .ToList();
                
                return VM;
            }
        }
    }
}