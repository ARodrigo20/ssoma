using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Commands.UpdateProceso
{
    public class UpdateProcesoCommand : IRequest<Unit>
    {
        public UpdateProcesoVM Data { get; set; }
        public class UpdateProcesoCommandHandler : IRequestHandler<UpdateProcesoCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateProcesoCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateProcesoCommand request, CancellationToken cancellationToken)
            {
                var data = request.Data;
                var obj = _context.TProceso.Find(data.CodProceso);
                obj.Descripcion = data.Descripcion;
                obj.CadenaAprobacion = String.Join<string>('.',data.Lista.Select(t => t.CodCadenaAprobacion));
                _context.TProceso.Update(obj);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}