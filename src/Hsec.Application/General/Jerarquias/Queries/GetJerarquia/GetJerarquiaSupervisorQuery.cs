using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Hsec.Application.General.Jerarquias.Queries.GetJerarquia
{
    public class GetJerarquiaSupervisorQuery : IRequest<List<SupervisorVM>>
    {
        public int CodPosicion { get; set; }
        public class GetJerarquiaSupervisorQueryHandler : IRequestHandler<GetJerarquiaSupervisorQuery, List<SupervisorVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetJerarquiaSupervisorQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<SupervisorVM>> Handle(GetJerarquiaSupervisorQuery request, CancellationToken cancellationToken)
            {
                //List<SupervisorVM> supervisores = new List<SupervisorVM>();

                var supervisores = _context.TJerarquia.Where(j => j.Estado && j.CodPosicionPadre.Equals(request.CodPosicion) && j.Tipo.Equals("O") && j.Visible);
                
                return _mapper.Map<List<SupervisorVM>>(supervisores);
            }
        }
    }
}