
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.TipoIncidente.Commands.UpdateTipoIncidente
{
    public partial class UpdateTipoIncidenteCommand : IRequest
    {
        public string CodTipoIncidente { get; set; }
        public string CodTipoIncidentePadre { get; set; }
        public string Descripcion { get; set; }

        public class UpdateTipoIncidenteCommandHandler : IRequestHandler<UpdateTipoIncidenteCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateTipoIncidenteCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateTipoIncidenteCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TTipoIncidente.FindAsync(request.CodTipoIncidente);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(TipoIncidente), request.CodTipoIncidente);
                }


                entity.CodPadreTipoIncidente = request.CodTipoIncidentePadre;
                entity.Descripcion = request.Descripcion;
               

                _context.TTipoIncidente.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);                

                return Unit.Value;
            }           
        }
    }
}
