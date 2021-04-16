using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Files.Commands.CreateOneFile
{
    public class CreateOneFileCommand : IRequest
    {
        public string NroDocReferencia { get; set; }
        public string NroSubDocReferencia { get; set; }
        public string CodTablaRef { get; set; }
        public IFormFile File { get; set; }

        public class CreateOneFileCommandHandler : IRequestHandler<CreateOneFileCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFileService IService;

            public CreateOneFileCommandHandler(IApplicationDbContext context, IFileService IService)
            {
                this._context = context;
                this.IService = IService;
            }

            public async Task<Unit> Handle(CreateOneFileCommand request, CancellationToken cancellationToken)
            {
                TFile file = new TFile();

                var fil = request.File;

                file.ArchivoData = trans(fil);
                file.TipoArchivo = fil.ContentType;
                file.Estado = true;
                file.Nombre = fil.FileName;
                file.Descripcion = (fil.Name == "nodesc") ? "" : fil.Name;
                file.NroDocReferencia = request.NroDocReferencia;
                file.NroSubDocReferencia = request.NroSubDocReferencia;
                file.CodTablaRef = request.CodTablaRef;
                if (file.TipoArchivo != null)
                {
                    if (file.TipoArchivo.Contains("image") || file.TipoArchivo.Contains("video") || file.TipoArchivo.Contains("pdf"))
                        file.PreviewData = await IService.getImagePreview(file);
                }
                _context.TFile.Add(file);

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

            private byte[] trans(IFormFile file)
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();

                }
                return fileBytes;
            }
        }
    }
}