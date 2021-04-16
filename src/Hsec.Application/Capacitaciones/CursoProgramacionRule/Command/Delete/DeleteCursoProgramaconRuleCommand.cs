using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Delete
{
    public class DeleteCursoProgramacionRuleCommand : IRequest<Unit>
    {
        public string recurrenceID { get; set; }
        public class DeleteCursoProgramacionCommandHandler : IRequestHandler<DeleteCursoProgramacionRuleCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeleteCursoProgramacionCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(DeleteCursoProgramacionRuleCommand request, CancellationToken cancellationToken)
            {
                var busquedaCurso = _context.TCursoRules.Include(i => i.TCurso).FirstOrDefault(i => i.RecurrenceID == request.recurrenceID && i.Estado);

                if (busquedaCurso != null)
                {
                    busquedaCurso.Estado = false;

                    if (busquedaCurso.TCurso.Count > 0)
                    {
                        var cursosAsoc = busquedaCurso.TCurso.Where(i => i.Estado).ToList();
                        foreach (var item in cursosAsoc)
                        {
                            item.Estado = false;
                        }
                    }
                    _context.TCursoRules.Update(busquedaCurso);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
