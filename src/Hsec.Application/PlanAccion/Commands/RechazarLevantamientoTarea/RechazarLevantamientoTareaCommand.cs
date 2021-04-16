using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.PlanAccion.Commands.RechazarLevantamientoTarea
{
    public class RechazarLevantamientoTareaCommand : IRequest<Unit>
    {
        public int CodAccion { get; set; }
        public class RechazarLevantamientoTareaCommandHandler : IRequestHandler<RechazarLevantamientoTareaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public RechazarLevantamientoTareaCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
            }
            public async Task<Unit> Handle(RechazarLevantamientoTareaCommand request, CancellationToken cancellationToken)
            {
                var levsTarea = _context.TLevantamientoPlan.Where(i => i.CodAccion == request.CodAccion && i.Estado);
                foreach (var lev in levsTarea)
                {
                    lev.Rechazado = true;
                    lev.PorcentajeAvance = 0;
                    _context.TLevantamientoPlan.Update(lev);
                }

                var planRelacionado = _context.TAccion.FirstOrDefault(i => i.CodAccion == request.CodAccion && i.Estado);

                if(planRelacionado != null)
                {
                    planRelacionado.CodEstadoAccion = "03";
                    _context.TAccion.Update(planRelacionado);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}