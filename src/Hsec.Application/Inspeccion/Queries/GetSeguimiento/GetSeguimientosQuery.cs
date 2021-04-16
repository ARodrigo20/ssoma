using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Inspeccion.Models;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System;

namespace Hsec.Application.Inspeccion.Queries.GetSeguimiento
{
    public class GetSeguimientosQuery : IRequest<List<Tuple<string, int>>>
    {
        public string modulo { get; set; }
        public List<string> personas { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }

        public class GetSeguimientosQueryHandler : IRequestHandler<GetSeguimientosQuery, List<Tuple<string, int>>>
        {
            private readonly IApplicationDbContext _context;

            public GetSeguimientosQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Tuple<string, int>>> Handle(GetSeguimientosQuery request, CancellationToken cancellationToken)
            {
                var codTipo = "0";
                if (request.modulo.Equals("02.01")) codTipo = "1";
                else if (request.modulo.Equals("02.02")) codTipo = "2";
                else if (request.modulo.Equals("02.03")) codTipo = "3";
                else if (request.modulo.Equals("02.04")) codTipo = "4";

                return  _context.TEquipoInspeccion.Join(_context.TInspeccion, jer => jer.CodInspeccion, jper => jper.CodInspeccion, (jer, jper) => new { jer = jer, jper = jper })
                 .Where(tuple => (tuple.jer.Estado && tuple.jper.Estado && tuple.jper.CodTipo==codTipo && tuple.jper.Fecha.Value.Year == request.anio && tuple.jper.Fecha.Value.Month == request.mes && request.personas.Contains(tuple.jer.CodPersona)))                 
                 .GroupBy(t => t.jer.CodPersona).Select(g => new Tuple<string, int>(g.Key, g.Count())).ToList();
                
            }
        }
    }
}
