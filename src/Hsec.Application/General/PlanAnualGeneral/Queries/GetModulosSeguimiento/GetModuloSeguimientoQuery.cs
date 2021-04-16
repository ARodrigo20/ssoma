using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetModulosSeguimiento
{
    public class GetModuloSeguimientoQuery : IRequest<int>
    {
        public string Modulo { get; set; }
        public string CodPersona { get; set; }
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public class GetModuloSeguimientoQueryHandler : IRequestHandler<GetModuloSeguimientoQuery, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetModuloSeguimientoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(GetModuloSeguimientoQuery request, CancellationToken cancellationToken)
            {

                var resp = 0;

                return  resp;
            }
        }
    }
}