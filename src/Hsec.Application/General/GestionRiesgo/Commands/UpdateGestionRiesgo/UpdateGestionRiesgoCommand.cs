
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.GestionRiesgo.Commands.UpdateGestionRiesgo
{
    public partial class UpdateGestionRiesgoCommand : IRequest
    {
        public string CodGestionRiesgo { get; set; }
        public string CodGestionRiesgoPadre { get; set; }
        public string Descripcion { get; set; }

        public class UpdateGestionRiesgoCommandHandler : IRequestHandler<UpdateGestionRiesgoCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateGestionRiesgoCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateGestionRiesgoCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TGestionRiesgo.FindAsync(request.CodGestionRiesgo);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(GestionRiesgo), request.CodGestionRiesgo);
                }


                entity.CodGestionRiesgoPadre = request.CodGestionRiesgoPadre;
                entity.Descripcion = request.Descripcion;
               

                _context.TGestionRiesgo.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);                

                return Unit.Value;
            }           
        }
    }
}
