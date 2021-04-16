using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.GestionRiesgo.Commands.CreateGestionRiesgo
{
    public partial class CreateGestionRiesgoCommand : IRequest<int>
    {
        public string CodGestionRiesgo { get; set; }
        public string CodGestionRiesgoPadre { get; set; }
        public string Descripcion { get; set; }

        public class CreateGestionRiesgoCommandHandler : IRequestHandler<CreateGestionRiesgoCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateGestionRiesgoCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateGestionRiesgoCommand request, CancellationToken cancellationToken)
            {
                var entity = new TGestionRiesgo();

                entity.CodGestionRiesgo = request.CodGestionRiesgo;
                entity.CodGestionRiesgoPadre = request.CodGestionRiesgoPadre;
                entity.Descripcion = request.Descripcion;

                _context.TGestionRiesgo.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return int.Parse(entity.CodGestionRiesgo);
            }
        }
    }
}
    

