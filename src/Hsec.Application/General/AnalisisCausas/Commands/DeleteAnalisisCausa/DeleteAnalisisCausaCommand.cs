
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.AnalisisCausas.Commands.UpdateAnalisisCausa
{
    public partial class DeleteAnalisisCausaCommand : IRequest
    {
        public string CodAnalisis { get; set; }

        public class DeleteAnalisisCausaCommanddHandler : IRequestHandler<DeleteAnalisisCausaCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteAnalisisCausaCommanddHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteAnalisisCausaCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TAnalisisCausa.FindAsync(request.CodAnalisis);

                if (entity == null) throw new GeneralFailureException("No existe Codigo en Base de datos");

                entity.Estado = false;

                _context.TAnalisisCausa.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);                

                return Unit.Value;
            }           
        }
    }
}
