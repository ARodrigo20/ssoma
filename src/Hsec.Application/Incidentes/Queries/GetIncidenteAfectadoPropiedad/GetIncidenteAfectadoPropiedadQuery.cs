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

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPropiedad
{
    
    public class GetIncidenteAfectadoPropiedadQuery : IRequest<List<PropiedadAfectadoDto>> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAfectadoPropiedadQueryHandler : IRequestHandler<GetIncidenteAfectadoPropiedadQuery, List<PropiedadAfectadoDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIncidenteAfectadoPropiedadQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<PropiedadAfectadoDto>> Handle(GetIncidenteAfectadoPropiedadQuery request, CancellationToken cancellationToken)
            {
                var CODIGO_INCIDENTE = request.CodIncidente;

                var objTIncidente = _context.TAfectadoPropiedad.Where(t => t.Estado==true && t.CodIncidente.Equals(CODIGO_INCIDENTE)).ToList();

                if (objTIncidente == null) throw new NotFoundException("Incidente",request.CodIncidente);
                
                var VM = _mapper.Map<List<TAfectadoPropiedad>,List<PropiedadAfectadoDto>>(objTIncidente);

                return VM;
            }
        }

    }
}