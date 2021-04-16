using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Expositores.Command.Update.VMs;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Expositores.Command.Update
{
    public class UpdateExpositorCommand : IRequest<Unit>
    {
        public UpdateExpositorVM VM { get; set; }
        public class UpdateExpositorCommandHandler : IRequestHandler<UpdateExpositorCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public UpdateExpositorCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(UpdateExpositorCommand request, CancellationToken cancellationToken)
            {
                var modelVM = request.VM;
                var expositor = _context.TExpositor.FirstOrDefault(i => i.CodCurso == modelVM.codTemaCapacita && i.CodPersona == modelVM.codPersona && i.Estado);
                if (expositor != null) {
                    expositor.Tipo = modelVM.tipo;
                    _context.TExpositor.Update(expositor);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
