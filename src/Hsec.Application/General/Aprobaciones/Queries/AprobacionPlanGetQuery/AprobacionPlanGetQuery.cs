using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Queries.AprobacionPlanGetQuery
{
    public class AprobacionPlanGetQuery : IRequest<AprobacionPlanVM>
    {
        public int CodAccion { get; set; }

        public class AprobacionPlanGetQueryHandler : IRequestHandler<AprobacionPlanGetQuery, AprobacionPlanVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public AprobacionPlanGetQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AprobacionPlanVM> Handle(AprobacionPlanGetQuery request, CancellationToken cancellationToken)
            {
                var res = _context.TAprobacionPlan
                    .Where(t => t.CodAccion == request.CodAccion && t.Estado == true)
                    .ProjectTo<AprobacionPlanVM>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();

                if(res != null)
                {
                    res.historial = _context.TAprobacionPlanHistorial
                    .Where(t => t.CodAprobacion == res.CodAprobacion && t.Estado == true)
                    .ProjectTo<AprobacionPlanHistorialVM>(_mapper.ConfigurationProvider)
                    .ToList();

                    foreach(var historial in res.historial)
                    {
                        var persona = _context.TPersona
                        .Where(p => p.CodPersona == historial.CodPersona)
                        .FirstOrDefault();

                        if(persona != null)
                        {
                            historial.Nombres = persona.Nombres + ", " + persona.ApellidoPaterno + " " + persona.ApellidoPaterno; 
                        }
                    }
                }
                return res;
            }
        }
    }
}