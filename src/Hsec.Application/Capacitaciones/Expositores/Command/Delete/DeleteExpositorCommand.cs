using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Expositores.Command.Delete
{
    public class DeleteExpositorCommand : IRequest<Unit>
    {
        public string codPersona { get; set; }
        public string codTemaCapacita { get; set; }
        public class DeleteExpositorCommandHandler : IRequestHandler<DeleteExpositorCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeleteExpositorCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(DeleteExpositorCommand request, CancellationToken cancellationToken)
            {
                var busquedaExpositor = _context.TExpositor.FirstOrDefault(i => i.CodCurso == request.codTemaCapacita && i.CodPersona == request.codPersona && i.Estado);

                if (busquedaExpositor != null)
                {
                    busquedaExpositor.Estado = false;
                    _context.TExpositor.Update(busquedaExpositor);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
