using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Commands.DeleteProceso
{
    public class DeleteProcesoCommand : IRequest<Unit>
    {
        public string CodProceso { get; set; }
        public class DeleteProcesoCommandHandler : IRequestHandler<DeleteProcesoCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public DeleteProcesoCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteProcesoCommand request, CancellationToken cancellationToken)
            {
                var CodPoceso = request.CodProceso;
                var Proceso = _context.TProceso.Where(t => t.CodProceso.Equals(CodPoceso)).FirstOrDefault();
                if(Proceso == null) throw new Exception("no existe campo");
                else{
                    Proceso.Estado = false;
                    _context.TProceso.Update(Proceso);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}