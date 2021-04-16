using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Aprobaciones.Commands.AprobacionPlanDelete;

namespace Hsec.Application.PlanAccion.Commands.DeletePlanDeAccion
{
    public class DeletePlanAccionCommand : IRequest<Unit>
    {
        public int CodAccion { get; set; }

        public class UpdatePlanAccionCommandHandler : IRequestHandler<DeletePlanAccionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;
            public UpdatePlanAccionCommandHandler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(DeletePlanAccionCommand request, CancellationToken cancellationToken)
            {
                var data = await _context.TAccion.Include(i => i.RespPlanAccion).FirstOrDefaultAsync(i => i.CodAccion == request.CodAccion && i.Estado);
                data.Estado = false;
                IList<TResponsable> listaResp = new List<TResponsable>();
                foreach (var item in data.RespPlanAccion)
                {
                    item.Estado = false;

                    var levTarea = _context.TLevantamientoPlan.Where(i => i.CodAccion == item.CodAccion && i.CodPersona == item.CodPersona && i.Estado);
                    foreach (var it in levTarea)
                    {
                        it.Estado = false;
                        _context.TLevantamientoPlan.Update(it);
                    }
                    listaResp.Add(item);
                }

                data.RespPlanAccion = listaResp;
                _context.TAccion.Update(data);
                await _context.SaveChangesAsync(cancellationToken);

                //var r1 = await _persons.DeleteAprobacionPlan(data.CodAccion);
                var r1 = await _mediator.Send(new AprobacionPlanDeleteCommand() { CodAccion = data.CodAccion });
                return Unit.Value;
            }
        }
    }
}
