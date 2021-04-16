using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Command.Delete
{
    public class DeleteCursoProgramacionCommand : IRequest<Unit>
    {
        public string codTemaCapacita { get; set; }
        public class DeleteCursoProgramacionCommandHandler : IRequestHandler<DeleteCursoProgramacionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeleteCursoProgramacionCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(DeleteCursoProgramacionCommand request, CancellationToken cancellationToken)
            {
                var busquedaCurso = _context.TCurso.Include(i => i.Expositores).Include(i => i.Participantes).FirstOrDefault(i => i.CodCurso == request.codTemaCapacita && i.Estado);

                if (busquedaCurso != null)
                {
                    busquedaCurso.Estado = false;

                    if (busquedaCurso.Participantes.Count > 0) {
                        var participantes = busquedaCurso.Participantes.Where(i => i.Estado).ToList();
                        foreach (var item in participantes) {
                            item.Estado = false;                            
                        }          
                    }

                    if (busquedaCurso.Expositores.Count > 0) {
                        var expositores = busquedaCurso.Expositores.Where(i => i.Estado).ToList();
                        foreach (var item in expositores) {
                            item.Estado = false;                        
                        }        
                    }
                    _context.TCurso.Update(busquedaCurso);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
