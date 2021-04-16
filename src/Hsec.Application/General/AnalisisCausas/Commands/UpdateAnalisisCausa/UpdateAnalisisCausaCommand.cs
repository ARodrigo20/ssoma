
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.AnalisisCausas.Commands.UpdateAnalisisCausa
{
    public partial class UpdateAnalisisCausaCommand : IRequest
    {
        public string CodAnalisis { get; set; }
        public string CodPadre { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }

        public class UpdateAnalisisCausaTreeCommandHandler : IRequestHandler<UpdateAnalisisCausaCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateAnalisisCausaTreeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateAnalisisCausaCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TAnalisisCausa.FindAsync(request.CodAnalisis);

                if (entity == null) throw new GeneralFailureException("No existe Codigo en Base de datos");

                entity.CodPadre = request.CodPadre;
                entity.Descripcion = request.Descripcion;                
                entity.Nivel = request.Nivel;

                _context.TAnalisisCausa.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);                

                return Unit.Value;
            }           
        }
    }
}
