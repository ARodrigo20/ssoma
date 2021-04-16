using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Delete
{
    public class DeletePlanTemaCommand : IRequest<Unit>
    {
        public string codReferencia { get; set; }
        public string codTemaCapacita { get; set; }
        public class DeletePlanTemaCommandHandler : IRequestHandler<DeletePlanTemaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeletePlanTemaCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(DeletePlanTemaCommand request, CancellationToken cancellationToken)
            {
                var busquedaPlanTema = _context.TPlanTema.FirstOrDefault(i => i.CodReferencia == request.codReferencia && i.CodTemaCapacita == request.codTemaCapacita && i.Estado);

                if (busquedaPlanTema != null) {
                    busquedaPlanTema.Estado = false;
                    _context.TPlanTema.Update(busquedaPlanTema);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
