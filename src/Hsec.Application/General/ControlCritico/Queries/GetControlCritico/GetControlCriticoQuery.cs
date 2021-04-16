using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.General.ControlCritico.Models;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.ControlCritico.Queries.GetControlCritico
{
    public class GetControlCriticoQuery : IRequest<ControlCriticoVM>
    {
        public string CodCC { get; set; }
        public class GetControlCriticoQueryHandler : IRequestHandler<GetControlCriticoQuery, ControlCriticoVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetControlCriticoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ControlCriticoVM> Handle(GetControlCriticoQuery request, CancellationToken cancellationToken)
            {
                TControlCritico controlCritico = _context.TControlCritico
                                    .Find(request.CodCC);

                if (controlCritico == null) throw new NotFoundException("control critico", request.CodCC);

                controlCritico.Criterios = _context.TCriterio.Where(t => t.Estado == true && t.CodCC.Equals(request.CodCC)).ToHashSet();

                ControlCriticoVM VM = new ControlCriticoVM();

                VM.data = _mapper.Map<TControlCritico, ControlCriticoDto>(controlCritico);


                return VM;
            }
        }

    }
}