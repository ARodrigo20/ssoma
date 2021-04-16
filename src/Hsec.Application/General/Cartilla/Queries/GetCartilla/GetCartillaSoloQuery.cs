using AutoMapper;
using Hsec.Application.General.Cartilla.Models;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Cartilla.Queries.GetCartilla
{
    public class GetCartillaSoloQuery : IRequest<CartillaDto>
    {
        public string CodCartilla { get; set; }
        public class GetCartillaSoloQueryHandler : IRequestHandler<GetCartillaSoloQuery, CartillaDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCartillaSoloQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CartillaDto> Handle(GetCartillaSoloQuery request, CancellationToken cancellationToken)
            {
                TCartilla obj = _context.TCartilla.Find(request.CodCartilla);

                if (obj != null && obj.Estado == false) throw new NotFoundException("Cartilla", request.CodCartilla);
                
                return _mapper.Map<TCartilla,CartillaDto>(obj);
            }
        }

    }
}