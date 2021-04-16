using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Maestro.Commands.DeleteMaestroData
{

    public class DeleteMaestroDataCommand : IRequest<Unit>
    {
        public string CodMaestro { get; set; }
        public string CodTabla { get; set; }


        public class DeleteMaestroDataCommandHandler : IRequestHandler<DeleteMaestroDataCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public DeleteMaestroDataCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteMaestroDataCommand request, CancellationToken cancellationToken)
            {

                TMaestro obsM = _context.TMaestro.Find(request.CodTabla, request.CodMaestro);
                obsM.Estado = false;
                _context.TMaestro.Update(obsM);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }


    }
}
