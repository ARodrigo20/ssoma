
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Jerarquias.Commands.DeleteJerarquias
{
    public partial class DeleteJerarquiaNodeCommand : IRequest
    {
        public int? CodPosicion { get; set; }    
        public class DeleteJerarquiaNodeCommandHandler : IRequestHandler<DeleteJerarquiaNodeCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteJerarquiaNodeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteJerarquiaNodeCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TJerarquia.FindAsync(request.CodPosicion);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Jerarquias), request.CodPosicion);
                }

                if (entity.Estado == true) {
                    entity.Estado = false;
                    _context.TJerarquia.Update(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else {                    
                    throw new DeleteEstadoRegistroException(nameof(Jerarquias), entity.Estado);
                }
                return Unit.Value;
            }           
        }
    }
}
