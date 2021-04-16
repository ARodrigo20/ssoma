using AutoMapper;
using Hsec.Application.General.Cartilla.Models;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Cartilla.Queries.GetCartilla
{
    public class GetCartillaQuery : IRequest<CartillaDto>
    {
        public string CodCartilla { get; set; }
        public class GetCartillaQueryHandler : IRequestHandler<GetCartillaQuery, CartillaDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCartillaQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CartillaDto> Handle(GetCartillaQuery request, CancellationToken cancellationToken)
            {
                TCartilla obj = _context.TCartilla.Find(request.CodCartilla);

                if (obj != null && obj.Estado == false) throw new NotFoundException("Cartilla", request.CodCartilla);
                
                obj.Detalle = _context.TCartillaDetalle
                    .Where(t => t.Estado == true && t.CodCartilla.Equals(request.CodCartilla))
                    .ToHashSet();
                
                return _mapper.Map<TCartilla,CartillaDto>(obj);
            }
        }

    }
}