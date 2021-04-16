using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Cartilla.Commands.DeleteCartilla
{
    public class DeleteCartillaCommand : IRequest<Unit>
    {
        public string CodCartilla { get; set; }
        public class DeleteCartillaCommandHandler : IRequestHandler<DeleteCartillaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public DeleteCartillaCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteCartillaCommand request, CancellationToken cancellationToken)
            {
                TCartilla obj = _context.TCartilla.Find(request.CodCartilla);

                if (obj != null && obj.Estado == false) throw new NotFoundException("Cartilla", request.CodCartilla);
                else
                {
                    obj.Estado = false;
                    var list = _context.TCartillaDetalle.Where(t => t.CodCartilla.Equals(request.CodCartilla));
                    foreach(var item in list)
                    {
                        item.Estado = false;
                    }
                    _context.TCartilla.Update(obj);
                    _context.TCartillaDetalle.UpdateRange(list);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }

    }
}