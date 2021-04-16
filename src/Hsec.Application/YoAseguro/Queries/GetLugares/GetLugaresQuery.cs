using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.YoAseguro.Models;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.YoAseguro;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Hsec.Application.YoAseguro.Queries.GetLugares
{
    public class GetLugaresQuery : IRequest<LugaresVM>
    {
        public string Cod { get; set; }
        public class GetLugaresQueryHandler : IRequestHandler<GetLugaresQuery, LugaresVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetLugaresQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LugaresVM> Handle(GetLugaresQuery request, CancellationToken cancellationToken)
            {
                var codigo = request.Cod;
                LugaresVM VM = new LugaresVM();
                if (codigo == "-1")
                {
                    VM.data = _context.TYoAseguroLugar
                    .Where(t => t.Estado == true && t.CodUbicacionPadre == null)
                    .ProjectTo<YoAseguroLugarDto>(_mapper.ConfigurationProvider)
                    .ToHashSet();

                    VM.size = VM.data.Count();
                } else
                {
                    VM.data = _context.TYoAseguroLugar
                    .Where(t => t.Estado == true && t.CodUbicacionPadre.Equals(request.Cod))
                    .ProjectTo<YoAseguroLugarDto>(_mapper.ConfigurationProvider)
                    .ToHashSet();

                    VM.size = VM.data.Count();
                }

                return VM;
            }
        }
    }
}
