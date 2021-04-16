using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Files.Commands.CreateFiles
{
    public class CreateListFilesCommand : IRequest
    {
        public string NroDocReferencia { get; set; }
        public string NroSubDocReferencia { get; set; }
        public string CodTablaRef { get; set; }
        public IFormFileCollection File { get; set; }

        public class CreateListFilesCommandHandler : IRequestHandler<CreateListFilesCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFileService IService;

            public CreateListFilesCommandHandler(IApplicationDbContext context, IFileService IService)
            {
                this._context = context;
                this.IService = IService;
            }

            public async Task<Unit> Handle(CreateListFilesCommand request, CancellationToken cancellationToken)
            {
                TFile file;

                IFormFileCollection files = request.File;

                foreach (var fil in files)
                {
                    file = new TFile();
                    if (fil.Length < 0) { throw new GeneralFailureException("Imagen no valida"); }
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
                }
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