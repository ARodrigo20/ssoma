using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.ControlCritico.Commands.DeleteControlCritico
{

    public class DeleteControlCriticoCommand : IRequest<Unit>
    {
        public string CodCC { get; set; }
        public class DeleteControlCriticoCommandHandler : IRequestHandler<DeleteControlCriticoCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public DeleteControlCriticoCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteControlCriticoCommand request, CancellationToken cancellationToken)
            {
                TControlCritico controlCritico = _context.TControlCritico
                    .Find(request.CodCC);

                if (controlCritico == null) throw new NotFoundException("control critico", request.CodCC);

                controlCritico.Estado = false;

                controlCritico.Criterios = _context.TCriterio.Where(t => t.Estado == true && t.CodCC.Equals(request.CodCC)).ToHashSet();

                foreach (var item in controlCritico.Criterios)
                    item.Estado = false;

                _context.TControlCritico.Update(controlCritico);

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }

      
    }
}