using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Files.Commands.DeleteFiles
{
    public class DeleteFileCommand : IRequest
    {
        public int CorrelativoArchivos { get; set; }
        public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFileService IService;

            public DeleteFileCommandHandler(IApplicationDbContext context, IFileService IService)
            {
                this._context = context;
                this.IService = IService;
            }

            public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
            {
                var data = await _context.TFile.FirstOrDefaultAsync(i => i.CorrelativoArchivos == request.CorrelativoArchivos);//.Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                data.Estado = false;
                _context.TFile.Update(data);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

        }
    }
}
