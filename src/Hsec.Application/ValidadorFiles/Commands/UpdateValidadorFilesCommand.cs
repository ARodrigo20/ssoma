using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.ValidadorFiles.Commands
{
    public class UpdateValidadorFilesCommand : IRequest<Unit>
    {
        public string nroDocReferencia { get; set; }
        public string codPersona { get; set; }
        public int codArchivo { get; set; }
        public class UpdateValidadorFilesCommandHandler : IRequestHandler<UpdateValidadorFilesCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateValidadorFilesCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateValidadorFilesCommand request, CancellationToken cancellationToken)
            {

                var TValFile = _context.TValidadorArchivo.FirstOrDefault(i => i.Estado && i.CodArchivo == request.codArchivo && i.CodPersona == request.codPersona && i.NroDocReferencia == request.nroDocReferencia);

                if (TValFile != null)
                {
                    TValFile.EstadoAccion = 2;
                    _context.TValidadorArchivo.Update(TValFile);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else {
                    throw new ExceptionGeneral("No se encuentra dicho dato !! ValidadorArchivo!!");                
                }
                return Unit.Value;
            }
        }
    }
}