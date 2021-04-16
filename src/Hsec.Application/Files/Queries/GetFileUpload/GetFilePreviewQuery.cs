using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Files.Queries.GetFileUpload
{
    public class GetFilePreviewQuery : IRequest<byte[]>
    {
        public int CorrelativoArchivos { get; set; }
        public byte[] PreviewData { get; set; }

        public class GetFilePreviewQueryHandler : IRequestHandler<GetFilePreviewQuery, byte[]>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetFilePreviewQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<byte[]> Handle(GetFilePreviewQuery request, CancellationToken cancellationToken)
            {
                var user = _context.TFile.Where(a => a.CorrelativoArchivos == request.CorrelativoArchivos).Select(a => a.PreviewData).FirstOrDefault();
                if (user!= null && user.Length>0 )
                {
                    return user;
                }
                else
                {
                    throw new ExceptionGeneral("EL ARCHIVO FUE ELIMINADO !");
                }
            }
        }
    }
}