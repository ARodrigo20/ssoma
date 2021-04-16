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
    public class CreateFileCommand : IRequest
    {
        public int CorrelativoArchivos { get; set; }
        public int GrupoPertenece { get; set; }
        public string Descripcion { get; set; }
        public string TipoArchivo { get; set; }
        public string Nombre { get; set; }
        public string NroDocReferencia { get; set; }
        public string NroSubDocReferencia { get; set; }
        public string CodTablaRef { get; set; } // agregue esto nuevo
        public bool Estado { get; set; }
        public IFormCollection File { get; set; }
        public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFileService IService;

            public CreateFileCommandHandler(IApplicationDbContext context, IFileService IService)
            {
                this._context = context;
                this.IService = IService;
            }

            public async Task<Unit> Handle(CreateFileCommand request, CancellationToken cancellationToken)
            {
                CreateFileCommand lista = IService.imgUpload(request.File);
                TFile file;


                IFormFileCollection files = request.File.Files;

                foreach (var fil in files)
                {
                    file = new TFile();
                    if (fil.Length < 0) { throw new GeneralFailureException("Imagen no valida"); }
                    file.ArchivoData = trans(fil);                    
                    file.TipoArchivo = fil.ContentType;
                    file.Estado = true;
                    file.Nombre = fil.FileName;                  
                    file.Descripcion = (fil.Name == "nodesc") ? "" : fil.Name;
                    file.NroDocReferencia = lista.NroDocReferencia;
                    file.NroSubDocReferencia = lista.NroSubDocReferencia;
                    file.CodTablaRef = lista.CodTablaRef;
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
