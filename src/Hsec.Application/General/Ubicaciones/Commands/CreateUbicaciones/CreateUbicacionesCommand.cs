using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Ubicaciones.Commands.CreateUbicaciones
{
    public partial class CreateUbicacionesCommand : IRequest<int>
    {
        public string CodUbicacion { get; set; }
        public string CodUbicacionPadre { get; set; }
        public string Descripcion { get; set; }

        public class CreateUbicacionesCommandHandler : IRequestHandler<CreateUbicacionesCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateUbicacionesCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateUbicacionesCommand request, CancellationToken cancellationToken)
            {
                var entity = new TUbicacion();

                entity.CodUbicacion = request.CodUbicacion;

                _context.TUbicacion.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return int.Parse(entity.CodUbicacion);
            }
        }
    }
}
    

