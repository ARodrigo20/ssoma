using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Update.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Update
{
    public class UpdatePlanTemaCommand : IRequest<Unit>
    {
        public UpdatePlanTemaRequestVM VM { get; set; }
        public class UpdatePlanTemaCommandHandler : IRequestHandler<UpdatePlanTemaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public UpdatePlanTemaCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(UpdatePlanTemaCommand request, CancellationToken cancellationToken)
            {
                var busquedaPlanTema = _context.TPlanTema.FirstOrDefault(i => i.CodReferencia == request.VM.codReferencia && i.Estado);

                if (busquedaPlanTema != null)
                {
                    var modelVM = request.VM;
                    busquedaPlanTema.CodTemaCapacita = modelVM.codTemaCapacita;
                    busquedaPlanTema.Tipo = modelVM.tipo;
                    _context.TPlanTema.Update(busquedaPlanTema);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
