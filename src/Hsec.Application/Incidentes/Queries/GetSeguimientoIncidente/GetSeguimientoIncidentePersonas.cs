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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Incidentes.Queries.GetSeguimientoIncidente
{
    public class GetSeguimientoIncidentePersonas : IRequest<List<Tuple<string, int>>>
    {
        public string modulo { get; set; }
        public List<string> personas { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }
        public class GetSeguimientoObseracionHandler : IRequestHandler<GetSeguimientoIncidentePersonas, List<Tuple<string, int>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSeguimientoObseracionHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<Tuple<string, int>>> Handle(GetSeguimientoIncidentePersonas request, CancellationToken cancellationToken)
            {
                return _context.TIncidente
                    .Where(
                        t => t.Estado == true
                        && request.personas.Contains(t.CodPerReporta)
                        && t.Creado.Year.Equals(request.anio)
                        && t.Creado.Month.Equals(request.mes)
                        )
                    .GroupBy(t => t.CodPerReporta)
                    .Select(g => new Tuple<string, int>(g.Key, g.Count()))
                    .ToList();
            }
        }
    }
}

