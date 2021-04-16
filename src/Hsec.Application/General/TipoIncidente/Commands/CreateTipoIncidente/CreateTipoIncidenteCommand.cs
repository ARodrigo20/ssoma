using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.TipoIncidente.Commands.CreateTipoIncidente
{
    public partial class CreateTipoIncidenteCommand : IRequest<int>
    {
        public string CodTipoIncidente { get; set; }
        public string CodTipoIncidentePadre { get; set; }
        public string Descripcion { get; set; }

        public class CreateTipoIncidenteCommandHandler : IRequestHandler<CreateTipoIncidenteCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateTipoIncidenteCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateTipoIncidenteCommand request, CancellationToken cancellationToken)
            {
                var entity = new TTipoIncidente();

                entity.CodTipoIncidente = request.CodTipoIncidente;
                entity.CodPadreTipoIncidente = request.CodTipoIncidentePadre;
                entity.Descripcion = request.Descripcion;

                _context.TTipoIncidente.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return int.Parse(entity.CodTipoIncidente);
            }
        }
    }
}
    

