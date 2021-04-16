using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Files.Queries.GetFilesUpload
{
    public class GetFilesAllQuery : IRequest<FileUploadAllVM>
    {
        public class GetFilesAllQueryHandler : IRequestHandler<GetFilesAllQuery, FileUploadAllVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetFilesAllQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FileUploadAllVM> Handle(GetFilesAllQuery request, CancellationToken cancellationToken)
            {
                var files = _context.TFile.AsQueryable();
                FileUploadAllVM archivos = new FileUploadAllVM();
                FilesUploadOneVM file;

                foreach (var item in files)
                {
                    if (item.Estado == true)
                    {
                        file = new FilesUploadOneVM();
                        file.CorrelativoArchivos = item.CorrelativoArchivos;
                        file.Descripcion = item.Descripcion;
                        file.Nombre = item.Nombre;
                        file.TipoArchivo = item.TipoArchivo;
                        archivos.data.Add(file);
                    }
                }
                archivos.count = archivos.data.Count;
                return archivos;
            }
        }
    }
}
