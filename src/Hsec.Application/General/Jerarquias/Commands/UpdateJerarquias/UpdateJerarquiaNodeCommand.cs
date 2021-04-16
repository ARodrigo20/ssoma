
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Jerarquias.Commands.UpdateJerarquias
{
    public partial class UpdateJerarquiaNodeCommand : IRequest
    {
        public int? CodPosicion { get; set; }       
        public string Description { get; set; }
        public string CodElipse { get; set; }
        public int? CodPadre { get; set; }
        public class UpdateJerarquiaNodeCommandHandler : IRequestHandler<UpdateJerarquiaNodeCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateJerarquiaNodeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateJerarquiaNodeCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TJerarquia.FindAsync(request.CodPosicion);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Jerarquias), request.CodPosicion);
                }
                
                if (entity.Estado == true)
                {
                    entity.Descripcion = request.Description;
                    entity.CodElipse = request.CodElipse;
                    entity.CodPosicionPadre = request.CodPadre;
                    _context.TJerarquia.Update(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                else {
                    throw new UpdateEstadoRegistroException(nameof(Jerarquias), entity.Estado);
                }
                return Unit.Value;
            }           
        }
    }
}
