using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hsec.Application.General.Aprobaciones.Commands.AprobacionPlanDelete
{
    public class AprobacionPlanDeleteCommand : IRequest<Unit>
    {
        public int CodAccion { get; set; }
        public class AprobacionPlanDeleteCommandHandler : IRequestHandler<AprobacionPlanDeleteCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;
            private readonly ICorreosService _sendCorreo;

            public AprobacionPlanDeleteCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContext, ICorreosService sendCorreo)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
                _sendCorreo = sendCorreo;
            }
            public async Task<Unit> Handle(AprobacionPlanDeleteCommand request, CancellationToken cancellationToken)
            {
                var data = request;

                var aprobacion = _context.TAprobacionPlan.Where(ap => ap.CodAccion == request.CodAccion).FirstOrDefault();

                if (aprobacion != null)
                {
                    aprobacion.Estado = false;
                    _context.TAprobacionPlan.Update(aprobacion);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}