using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Delete
{
    public class DeleteTemaCapacitacionCommand :IRequest
    {
        public string codTemaCapacita { get; set; }
        public class DeleteTemaCapacitacionCommandHandler : IRequestHandler<DeleteTemaCapacitacionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeleteTemaCapacitacionCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(DeleteTemaCapacitacionCommand request, CancellationToken cancellationToken)
            {
                var busquedaTema = _context.TTemaCapacitacion.Include(i => i.TemaCapEspecifico).Include(i => i.PlanTema).FirstOrDefault(i => i.CodTemaCapacita == request.codTemaCapacita && i.Estado);
                busquedaTema.Estado = false;

                var temaCapCount = busquedaTema.TemaCapEspecifico.Count();
                var temaPlanCount = busquedaTema.PlanTema.Count();

                if (temaCapCount > 0) {             
                    foreach (var item in busquedaTema.TemaCapEspecifico)
                    {
                        if (item.Estado) {
                            item.Estado = false;
                        }
                    }
                }

                if (temaPlanCount > 0) {
                    foreach (var item in busquedaTema.PlanTema)
                    {
                        if (item.Estado) {
                            item.Estado = false;
                        }
                    }
                }
                _context.TTemaCapacitacion.Update(busquedaTema);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
