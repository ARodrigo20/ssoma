using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.General.ControlCritico.Models;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.ControlCritico.Commands.UpdateControlCitico
{

    public class UpdateControlCriticoCommand : IRequest<Unit>
    {
        public string CodCC { get; set; }
        public ControlCriticoDto data { get; set; }
        public class UpdateControlCriticoCommandHandler : IRequestHandler<UpdateControlCriticoCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateControlCriticoCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateControlCriticoCommand request, CancellationToken cancellationToken)
            {
                TControlCritico controlCritico = _context.TControlCritico
                    .Find(request.CodCC);

                if (controlCritico == null) throw new NotFoundException("control critico", request.CodCC);

                controlCritico.Criterios = _context.TCriterio.Where(t => t.Estado == true && t.CodCC.Equals(request.CodCC)).ToHashSet();

                controlCritico = _mapper.Map<ControlCriticoDto, TControlCritico>(request.data, controlCritico);
                _context.TControlCritico.Update(controlCritico);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }

      
    }
}