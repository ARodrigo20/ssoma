using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Files.Commands.UpdateFiles
{
    public class UpdateFileCommand : IRequest
    {
        public int CorrelativoArchivos { get; set; }
        public string Descripcion { get; set; }

        public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFileService IService;

            public UpdateFileCommandHandler(IApplicationDbContext context, IFileService IService)
            {
                this._context = context;
                this.IService = IService;
            }

            public async Task<Unit> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
            {
                var data = await _context.TFile.FirstOrDefaultAsync(i => i.CorrelativoArchivos == request.CorrelativoArchivos);//.Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                data.Descripcion = request.Descripcion;
                _context.TFile.Update(data);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
