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

namespace Hsec.Application.Inspeccion.Queries.GetSeguimiento
{
    public class GetSeguimientoQuery : IRequest<int>
    {
        public string modulo { get; set; }
        public string persona { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }
        public string usuario {get;set;}
        public string rol {get;set;}

        public class GetSeguimientoQueryHandler : IRequestHandler<GetSeguimientoQuery, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSeguimientoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(GetSeguimientoQuery request, CancellationToken cancellationToken)
            {
                var codTipo = "0";
                if (request.modulo.Equals("02.01")) codTipo = "1";
                else if (request.modulo.Equals("02.02")) codTipo = "2";
                else if (request.modulo.Equals("02.03")) codTipo = "3";
                else if (request.modulo.Equals("02.04")) codTipo = "4";

                if(request.rol.Equals("6")){
                    return _context.TEquipoInspeccion
                    .Join(_context.TInspeccion, 
                        equIns => equIns.CodInspeccion, 
                        ins => ins.CodInspeccion, 
                        (equIns, ins) => new { equIns = equIns, ins = ins })
                    .Where(tuple => (
                        tuple.equIns.Estado && tuple.ins.Estado 
                        && tuple.ins.CodTipo == codTipo 
                        && tuple.ins.Fecha.Value.Year == request.anio 
                        && tuple.ins.Fecha.Value.Month == request.mes 
                        && request.usuario==tuple.equIns.CreadoPor))
                    .Count();
                }
                else{
                    return _context.TEquipoInspeccion
                    .Join(_context.TInspeccion, 
                        equIns => equIns.CodInspeccion, 
                        ins => ins.CodInspeccion, 
                        (equIns, ins) => new { equIns = equIns, ins = ins })
                    .Where(tuple => (
                        tuple.equIns.Estado && tuple.ins.Estado 
                        && tuple.ins.CodTipo == codTipo 
                        && tuple.ins.Fecha.Value.Year == request.anio 
                        && tuple.ins.Fecha.Value.Month == request.mes 
                        && request.persona==tuple.equIns.CodPersona))
                    .Count();
                }
            }
        }
    }
}
