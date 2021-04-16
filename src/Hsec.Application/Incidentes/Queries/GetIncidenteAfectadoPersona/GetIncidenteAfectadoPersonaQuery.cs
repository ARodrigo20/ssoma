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

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPersona
{
    
    public class GetIncidenteAfectadoPersonaQuery : IRequest<List<PersonaAfectadoDto>> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAfectadoPersonaQueryHandler : IRequestHandler<GetIncidenteAfectadoPersonaQuery, List<PersonaAfectadoDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIncidenteAfectadoPersonaQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<PersonaAfectadoDto>> Handle(GetIncidenteAfectadoPersonaQuery request, CancellationToken cancellationToken)
            {
                var CODIGO_INCIDENTE = request.CodIncidente;

                var objTIncidentePer = _context.TInvestigaAfectado.Where(t => t.Estado==true && t.CodIncidente.Equals(CODIGO_INCIDENTE)).ToList();

                if (objTIncidentePer == null) throw new NotFoundException("Incidente",request.CodIncidente);

                var VM = _mapper.Map<List<TInvestigaAfectado>,List<PersonaAfectadoDto>>(objTIncidentePer);

                foreach (var item in VM)
                {
                    item.Men = _context.TDiasPerdidosAfectado
                        .Where(t => t.Estado==true && t.CodIncidente.Equals(CODIGO_INCIDENTE) && t.CodPersona.Equals(item.DocAfectado))
                        .ProjectTo<MenAfectadoPersonaDto>(_mapper.ConfigurationProvider)
                        .ToList();
                }

                return VM;
            }
        }

    }
}