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

namespace Hsec.Application.Incidentes.Queries.GetIncidenteGeneral
{

    public class GetIncidenteGeneralQuery : IRequest<DatosGeneralesDto>
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteGeneralQueryHandler : IRequestHandler<GetIncidenteGeneralQuery, DatosGeneralesDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIncidenteGeneralQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<DatosGeneralesDto> Handle(GetIncidenteGeneralQuery request, CancellationToken cancellationToken)
            {
                var VM = new DatosGeneralesDto();
                var CODIGO_INCIDENTE = request.CodIncidente;

                TIncidente objTIncidente = _context.TIncidente.Find(request.CodIncidente);

                if (objTIncidente == null || objTIncidente.Estado == false) throw new NotFoundException("Incidente", request.CodIncidente);

                VM = _mapper.Map<TIncidente, DatosGeneralesDto>(objTIncidente);

                return VM;
            }
        }
    }
}