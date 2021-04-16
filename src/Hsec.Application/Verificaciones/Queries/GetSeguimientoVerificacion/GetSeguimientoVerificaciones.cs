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

namespace Hsec.Application.Verificaciones.Queries.GetSeguimientoVerificaciones
{
    public class GetSeguimientoVerificaciones : IRequest<List<Tuple<string, int>>>
    {
        public string modulo { get; set; }
        public List<string> personas { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }
        public class GetSeguimientoVerificacionHandler : IRequestHandler<GetSeguimientoVerificaciones, List<Tuple<string, int>>>
        {
            private readonly IApplicationDbContext _context;

            public GetSeguimientoVerificacionHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Tuple<string, int>>> Handle(GetSeguimientoVerificaciones request, CancellationToken cancellationToken)
            {

                string modulo;
                if (request.modulo.Equals("12.01")) modulo = TipoVerificacion.IPERC_Continuo;
                else if (request.modulo.Equals("12.02")) modulo = TipoVerificacion.PTAR;
                else return null;

                return _context.TVerificaciones.Where(
                    t => t.Estado
                    && request.personas.Contains(t.CodVerificacionPor)
                    && t.CodTipoVerificacion.Equals(modulo)
                    && t.FechaVerificacion.Year.Equals(request.anio)
                    && t.FechaVerificacion.Month.Equals(request.mes)
                    ).GroupBy(t => t.CodVerificacionPor).Select(g => new Tuple<string, int>(g.Key, g.Count())).ToList();

                //if(string.IsNullOrEmpty(request.modulo)) return 0;

                //if(request.modulo.Equals("12")){
                //    return _context.TVerificaciones.Where(
                //        t => t.Estado == true 
                //        && t.CodVerificacion.Equals(request.persona) 
                //        && t.FechaVerificacion.Year.Equals(request.anio)
                //        && t.FechaVerificacion.Month.Equals(request.mes)
                //        )
                //        .Count();
                //}
                //else{
                //}
            }
        }
    }
}