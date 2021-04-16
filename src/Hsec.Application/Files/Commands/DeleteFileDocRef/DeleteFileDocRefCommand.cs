using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Files.Commands.DeleteFileDocRef
{
    public class DeleteFileDocRefCommand : IRequest
    {
        public string NroDocReferencia { get; set; }
        public class DeleteFileDocRefCommandHandler : IRequestHandler<DeleteFileDocRefCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFileService IService;

            public DeleteFileDocRefCommandHandler(IApplicationDbContext context, IFileService IService)
            {
                this._context = context;
                this.IService = IService;
            }

            public async Task<Unit> Handle(DeleteFileDocRefCommand request, CancellationToken cancellationToken)
            {
                var data = _context.TFile.Where(i => i.NroDocReferencia == request.NroDocReferencia && i.Estado);//.Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                foreach (var item in data)
                {
                    item.Estado = false;
                    _context.TFile.Update(item);
                }

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

        }
    }
}
