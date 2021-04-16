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

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoComunidad
{
    
    public class GetIncidenteAfectadoComunidadQuery : IRequest<List<ComunidadAfectadoDto>> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAfectadoComunidadQueryHandler : IRequestHandler<GetIncidenteAfectadoComunidadQuery, List<ComunidadAfectadoDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIncidenteAfectadoComunidadQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ComunidadAfectadoDto>> Handle(GetIncidenteAfectadoComunidadQuery request, CancellationToken cancellationToken)
            {
                var CODIGO_INCIDENTE = request.CodIncidente;
                // var objTAC = _context.TAfectadoComunidad.Find(CORRELATIVO,CODIGO_INCIDENTE);
                var objTAC = _context.TAfectadoComunidad.Where(t => t.Estado==true && t.CodIncidente.Equals(CODIGO_INCIDENTE)).ToList();;
                if (objTAC == null ) throw new NotFoundException("Incidente", request.CodIncidente);
                var VM = _mapper.Map< List<TAfectadoComunidad> ,List<ComunidadAfectadoDto> >(objTAC);
                return VM;
            }
        }

    }
}