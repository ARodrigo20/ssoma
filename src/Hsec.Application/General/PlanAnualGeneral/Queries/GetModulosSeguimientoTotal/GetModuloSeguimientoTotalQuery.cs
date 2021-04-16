using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetModulosSeguimientoTotal
{
    public class GetModuloSeguimientoTotalQuery : IRequest<int>
    {
        public string Modulo { get; set; }
        public int Gerencia { get; set; }
        public string Anio { get; set; }
        public string CodMes { get; set; }

        public class GetModuloSeguimientoTotalQueryHandler : IRequestHandler<GetModuloSeguimientoTotalQuery, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetModuloSeguimientoTotalQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(GetModuloSeguimientoTotalQuery request, CancellationToken cancellationToken)
            {

                var resp = 0;

                return resp;
            }
        }
    }
}