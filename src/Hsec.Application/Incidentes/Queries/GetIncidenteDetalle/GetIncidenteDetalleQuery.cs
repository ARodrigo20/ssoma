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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteDetalle
{
    
    public class GetIncidenteDetalleQuery : IRequest<DetalleDto> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteDetalleQueryHandler : IRequestHandler<GetIncidenteDetalleQuery, DetalleDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIncidenteDetalleQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<DetalleDto> Handle(GetIncidenteDetalleQuery request, CancellationToken cancellationToken)
            {
                var VM = new DetalleDto();
                var CODIGO_INCIDENTE = request.CodIncidente;

                TIncidente objTIncidente = _context.TIncidente.Find(request.CodIncidente);

                if (objTIncidente == null || objTIncidente.Estado == false) throw new NotFoundException("Incidente",request.CodIncidente);

                VM = _mapper.Map<DetalleDto>(objTIncidente);

                VM.EquipoInvestigacion = _context.TEquipoInvestigacion
                    .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                    .ProjectTo<EquipoInvestigacionDetalleDto>(_mapper.ConfigurationProvider)
                    .ToList();

                VM.SecuenciaEventos = _context.TSecuenciaEvento
                    .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                    .ProjectTo<SecuenciaEventosDetalleDto>(_mapper.ConfigurationProvider)
                    .ToList();

                VM.TestigosInvolucrados = _context.TTestigoInvolucrado
                    .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                    .ProjectTo<TestigosInvolucradosDetalleDto>(_mapper.ConfigurationProvider)
                    .ToList();

                
                return VM;
            }
        }

    }
}