using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Hsec.Application.General.Maestro.Commands.CreateMaestroData
{

    public class CreateMaestroDataCommand : IRequest<int>, IMapFrom<TMaestro>
    {

        public string CodTabla { get; set; }
        public string CodTtipo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }


        public class CreateMaestroDataCommandHandler : IRequestHandler<CreateMaestroDataCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateMaestroDataCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateMaestroDataCommand request, CancellationToken cancellationToken)
            {
                

                TMaestro obsM = _mapper.Map<TMaestro>(request);

                var maxCod = _context.TMaestro.Where(t => t.CodTabla.Equals("ControlCriticoPF")).Max(m => m.CodTipo);
                if (maxCod == null) maxCod = "CCPF0000001";
                else
                {
                    int id = int.Parse(maxCod.Substring(4, 7)) + 1;
                    maxCod = "CCPF" + id.ToString("D7");
                }

                obsM.CodTipo = maxCod;

                _context.TMaestro.Add(obsM);
                await _context.SaveChangesAsync(cancellationToken);

                return 0;
            }
        }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMaestroDataCommand, TMaestro>();
        }
    }
}
