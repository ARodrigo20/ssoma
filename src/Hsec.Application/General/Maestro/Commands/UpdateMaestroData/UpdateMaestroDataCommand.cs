using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Maestro.Commands.UpdateMaestroData
{

    public class UpdateMaestroDataCommand : IRequest<int>
    {
        public string CodMaestro { get; set; }
        public string Descripcion { get; set; }
        public string CodTabla { get; set; }


        public class UpdateMaestroDataCommandHandler : IRequestHandler<UpdateMaestroDataCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateMaestroDataCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(UpdateMaestroDataCommand request, CancellationToken cancellationToken)
            {
                TMaestro obsM = _context.TMaestro.Find(request.CodTabla, request.CodMaestro);

                if (obsM == null || obsM.Estado == false)
                {
                    throw new NotFoundException(request.CodTabla, request.CodMaestro);
                }

                obsM.Descripcion = request.Descripcion;
                _context.TMaestro.Update(obsM);
                await _context.SaveChangesAsync(cancellationToken);

                return 0;
            }
        }


    }
}
