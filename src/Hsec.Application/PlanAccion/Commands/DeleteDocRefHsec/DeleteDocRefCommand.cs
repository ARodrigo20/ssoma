using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.PlanAccion.Commands.DeleteDocRefHsec
{
    public class DeleteDocRefCommand : IRequest
    {
        public string? NroDocReferencia { get; set; }
        public class DeleteFileCommandHandler : IRequestHandler<DeleteDocRefCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFileService IService;

            public DeleteFileCommandHandler(IApplicationDbContext context, IFileService IService)
            {
                this._context = context;
                this.IService = IService;
            }

            public async Task<Unit> Handle(DeleteDocRefCommand request, CancellationToken cancellationToken)
            {
                var data = _context.TAccion.AsQueryable().Where(i => i.DocReferencia == request.NroDocReferencia && i.Estado);//.Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                foreach (var item in data)
                {
                    item.Estado = false;
                    _context.TAccion.Update(item);
                }

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

        }
    }
}
