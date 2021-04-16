
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Ubicaciones.Commands.UpdateUbicaciones
{
    public partial class UpdateUbicacionesCommand : IRequest
    {
        public string CodUbicacion { get; set; }
        public string CodUbicacionPadre { get; set; }
        public string Descripcion { get; set; }

        public class UpdateUbicacionesCommandHandler : IRequestHandler<UpdateUbicacionesCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateUbicacionesCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateUbicacionesCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TUbicacion.FindAsync(request.CodUbicacion);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Ubicaciones), request.CodUbicacion);
                }


                entity.CodUbicacionPadre = request.CodUbicacionPadre;
                entity.Descripcion = request.Descripcion;
               

                _context.TUbicacion.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);                

                return Unit.Value;
            }           
        }
    }
}
