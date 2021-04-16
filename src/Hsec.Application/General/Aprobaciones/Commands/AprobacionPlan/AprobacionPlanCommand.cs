using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hsec.Application.General.Aprobaciones.Commands.AprobacionPlan
{
    public class AprobacionPlanCommand : IRequest<Unit>
    {
        public int CodAccion { get; set; }
        public string DocReferencia { get; set; }
        public string CodAprobador { get; set; }
        public string Comentario { get; set; }
        public string EstadoAprobacion { get; set; }
        public string CodTabla { get; set; }

        public class AprobacionPlanCommandHandler : IRequestHandler<AprobacionPlanCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;
            private readonly ICorreosService _sendCorreo;

            public AprobacionPlanCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContext, ICorreosService sendCorreo)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
                _sendCorreo = sendCorreo;
            }
            public async Task<Unit> Handle(AprobacionPlanCommand request, CancellationToken cancellationToken)
            {
                var data = request;

                var aprobacion = _context.TAprobacionPlan.Where(ap => ap.CodAccion == request.CodAccion && ap.Estado).FirstOrDefault();

                if(aprobacion == null)
                {
                    TAprobacionPlan newAprobacion = new TAprobacionPlan();
                    newAprobacion.CodAccion = request.CodAccion;
                    newAprobacion.DocReferencia = request.DocReferencia;
                    //newAprobacion.CodAprobador = request.CodAprobador;
                    //newAprobacion.Comentario = request.Comentario;
                    newAprobacion.EstadoDoc = request.EstadoAprobacion;
                    newAprobacion.CodTabla = request.CodTabla;
                    _context.TAprobacionPlan.Add(newAprobacion);

                    TAprobacionPlanHistorial historial = new TAprobacionPlanHistorial();
                    historial.CodAprobacion = newAprobacion.CodAprobacion;
                    historial.Comentario = request.Comentario;
                    historial.CodPersona = request.CodAprobador;
                    historial.EstadoAprobacion = request.EstadoAprobacion;
                    _context.TAprobacionPlanHistorial.Add(historial);
                    TAccion plan = _context.TAccion.Find(request.CodAccion);
                    plan.EstadoAprobacion = request.EstadoAprobacion;
                    _context.TAccion.Update(plan);
                }
                else
                {
                    //aprobacion.CodAprobador = request.CodAprobador;
                    //aprobacion.Comentario = request.Comentario;
                    aprobacion.EstadoDoc = request.EstadoAprobacion;
                    _context.TAprobacionPlan.Update(aprobacion);

                    TAprobacionPlanHistorial historial = new TAprobacionPlanHistorial();
                    historial.CodAprobacion = aprobacion.CodAprobacion;
                    historial.Comentario = request.Comentario;
                    historial.CodPersona = request.CodAprobador;
                    historial.EstadoAprobacion = request.EstadoAprobacion;
                    _context.TAprobacionPlanHistorial.Add(historial);
                    TAccion plan = _context.TAccion.Find(request.CodAccion);
                    plan.EstadoAprobacion = request.EstadoAprobacion;
                    _context.TAccion.Update(plan);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}