using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hsec.Application.General.Aprobaciones.Commands.AprobacionPlanCreate
{
    public class AprobacionPlanCreateCommand : IRequest<Unit>
    {
        public int CodAccion { get; set; }
        public string DocReferencia { get; set; }
        public string CodAprobador { get; set; }
        public string Comentario { get; set; }
        public string EstadoAprobacion { get; set; }
        public string CodTabla { get; set; }

        public class AprobacionPlanCreateCommandHandler : IRequestHandler<AprobacionPlanCreateCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;
            private readonly ICorreosService _sendCorreo;

            public AprobacionPlanCreateCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContext, ICorreosService sendCorreo)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
                _sendCorreo = sendCorreo;
            }
            public async Task<Unit> Handle(AprobacionPlanCreateCommand request, CancellationToken cancellationToken)
            {
                TAprobacionPlan newAprobacion = new TAprobacionPlan();
                newAprobacion.CodAccion = request.CodAccion;
                newAprobacion.DocReferencia = request.DocReferencia;
                //newAprobacion.CodAprobador = request.CodAprobador;
                //newAprobacion.Comentario = request.Comentario;
                newAprobacion.EstadoDoc = "P";
                newAprobacion.CodTabla = request.CodTabla;
                _context.TAprobacionPlan.Add(newAprobacion);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}