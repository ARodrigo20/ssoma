using Hsec.Application.Common.Interfaces;
using Hsec.Application.Files.Commands.UpdateFiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Files.Commands.UpdateFiles
{
    public class UpdateFilesCommand : IRequest
    {
        public FileListUpdateVM doc { get; set; }

        public class UpdateFilesCommandHandler : IRequestHandler<UpdateFilesCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            public UpdateFilesCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(UpdateFilesCommand request, CancellationToken cancellationToken)
            {
               
                foreach (var item in request.doc.files)
                {
                    var data = await _context.TFile.FirstOrDefaultAsync(i => i.CorrelativoArchivos == item.correlativoArchivos);//.Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                    data.Descripcion = item.descripcion;
                    data.Estado = item.estado;
                    _context.TFile.Update(data);
                }
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
