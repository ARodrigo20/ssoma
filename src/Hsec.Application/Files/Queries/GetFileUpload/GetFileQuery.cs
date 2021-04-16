using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.ValidadorFiles.Commands.VMs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Files.Queries.GetFileUpload
{
    public class GetFileQuery : IRequest<FileVM>
    {
        public int CorrelativoArchivos { get; set; }
        public byte[] ArchivoData { get; set; }      
        public string TipoArchivo { get; set; }

        public class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetFileQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FileVM> Handle(GetFileQuery request, CancellationToken cancellationToken)
            {
                var user = _context.TFile.FirstOrDefault(a => a.CorrelativoArchivos == request.CorrelativoArchivos && a.Estado);
                if (user != null)
                {
                    FileVM fileVM = new FileVM();
                    byte[] imgbyte = user.ArchivoData;
                    if (user == null)
                    {
                        throw new ExceptionGeneral(user.ToString());
                    }
                    fileVM.archivoData = user.ArchivoData;
                    fileVM.nombre = user.Nombre;
                    fileVM.tipoArchivo = user.TipoArchivo;
                    return fileVM;
                }
                else
                {
                    throw new ExceptionGeneral("EL ARCHIVO FUE ELIMINADO !");
                }
            }
        }
    }
}