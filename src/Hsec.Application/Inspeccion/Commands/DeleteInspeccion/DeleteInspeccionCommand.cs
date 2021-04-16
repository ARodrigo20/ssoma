using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.Inspecciones;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Hsec.Application.Inspeccion.Commands.DeleteInspeccion
{
    public class DeleteInspeccionCommand : IRequest<Unit>
    {
        public string CodInspeccion { get; set; }
        public class DeleteInspeccionCommandHandler : IRequestHandler<DeleteInspeccionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeleteInspeccionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteInspeccionCommand request, CancellationToken cancellationToken)
            {

                TInspeccion entityInspeccion = _context.TInspeccion.Find(request.CodInspeccion);

                if (entityInspeccion != null && entityInspeccion.Estado == false) throw new NotFoundException("Inspeccion", request.CodInspeccion);
                else
                {
                    entityInspeccion.Estado = false;
                    _context.TInspeccion.Update(entityInspeccion);

                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;

            }
        }
    }
}