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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Incidentes.Queries.GetSeguimientoIncidente
{
    public class GetSeguimientoIncidente : IRequest<int>
    {
        public string modulo { get; set; }
        public string persona { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }
        public string usuario {get;set;}
        public string rol {get;set;}
        public class GetSeguimientoIncidenteHandler : IRequestHandler<GetSeguimientoIncidente, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSeguimientoIncidenteHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(GetSeguimientoIncidente request, CancellationToken cancellationToken)
            {
                if(request.rol.Equals("6")){
                    return _context.TIncidente.Where(
                            t => t.Estado == true 
                            && t.CreadoPor.Equals(request.usuario) 
                            && t.FechaDelSuceso.Value.Year.Equals(request.anio)
                            && t.FechaDelSuceso.Value.Month.Equals(request.mes)
                            )
                            .Count();
                }
                else{
                    return _context.TIncidente.Where(
                            t => t.Estado == true 
                            && t.CodPerReporta.Equals(request.persona) 
                            && t.FechaDelSuceso.Value.Year.Equals(request.anio)
                            && t.FechaDelSuceso.Value.Month.Equals(request.mes)
                            )
                            .Count();
                }
                
            }
        }
    }
}