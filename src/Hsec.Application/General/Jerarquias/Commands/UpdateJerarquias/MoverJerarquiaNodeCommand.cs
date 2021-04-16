
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Jerarquias.Commands.UpdateJerarquias
{
    public partial class MoverJerarquiaNodeCommand : IRequest
    {
        public int? CodPosicion { get; set; }       
        public int? CodNodeParentNuevo { get; set; }     

        public class MoverJerarquiaNodeCommandHandler : IRequestHandler<MoverJerarquiaNodeCommand>
        {
            private readonly IApplicationDbContext _context;

            public MoverJerarquiaNodeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(MoverJerarquiaNodeCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TJerarquia.FindAsync(request.CodPosicion);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Jerarquias), request.CodNodeParentNuevo);
                }
                if (entity.Estado == true)
                {
                    entity.CodPosicionPadre = request.CodNodeParentNuevo;
                    _context.TJerarquia.Update(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    throw new UpdateEstadoRegistroException(nameof(Jerarquias), entity.Estado);
                }

                return Unit.Value;
            }           
        }
    }
}
