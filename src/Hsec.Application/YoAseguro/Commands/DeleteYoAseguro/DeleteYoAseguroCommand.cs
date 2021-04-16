using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.YoAseguro;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.YoAseguro.Commands.DeleteYoAseguro
{
    public class DeleteYoAseguroCommand : IRequest<Unit>
    {
        public string CodYoAseguro { get; set; }
        public class DeleteYoAseguroCommandHandler : IRequestHandler<DeleteYoAseguroCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            public DeleteYoAseguroCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteYoAseguroCommand request, CancellationToken cancellationToken)
            {
                TYoAseguro entityYoA = _context.TYoAseguro.Find(request.CodYoAseguro);

                if (entityYoA != null && entityYoA.Estado == false) throw new NotFoundException("YoAseguro", request.CodYoAseguro);
                else
                {
                    entityYoA.Estado = false;
                    _context.TYoAseguro.Update(entityYoA);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
