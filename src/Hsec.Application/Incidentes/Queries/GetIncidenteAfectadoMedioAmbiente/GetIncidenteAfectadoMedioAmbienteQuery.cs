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

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoMedioAmbiente
{
    
    public class GetIncidenteAfectadoMedioAmbienteQuery : IRequest<List<MedioAmbienteAfectadoDto>> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAfectadoMedioAmbienteQueryHandler : IRequestHandler<GetIncidenteAfectadoMedioAmbienteQuery, List<MedioAmbienteAfectadoDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIncidenteAfectadoMedioAmbienteQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<MedioAmbienteAfectadoDto>> Handle(GetIncidenteAfectadoMedioAmbienteQuery request, CancellationToken cancellationToken)
            {
                var CODIGO_INCIDENTE = request.CodIncidente;
                var objTIncidenteMA = _context.TAfectadoMedioAmbiente.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE)).ToList();
                if (objTIncidenteMA == null) throw new NotFoundException("Incidente",request.CodIncidente);
                var VM = _mapper.Map<List<TAfectadoMedioAmbiente>,List<MedioAmbienteAfectadoDto>>(objTIncidenteMA);

                return VM;
            }
        }

    }
}