using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Create.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Create
{
    public class CreatePlanTemaCommand : IRequest
    {
        public CreatePlanTemaRequestVM VM { get; set; }
        public class CreatePlanTemaCommandHandler : IRequestHandler<CreatePlanTemaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreatePlanTemaCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(CreatePlanTemaCommand request, CancellationToken cancellationToken)
            {
                var temPlanTemaVM = request.VM;
                var codTemaCapacita = temPlanTemaVM.codTemaCapacita;
                TPlanTema planTemaEntity = new TPlanTema();
                
                planTemaEntity.CodReferencia = temPlanTemaVM.codReferencia;
                planTemaEntity.CodTemaCapacita = codTemaCapacita;
                planTemaEntity.Estado = true;
                planTemaEntity.Tipo = temPlanTemaVM.tipo;
                _context.TPlanTema.Add(planTemaEntity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
