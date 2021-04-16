using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Queries.GetAprobacionesTabla
{
    public class GetAprobacionesTablaQuery : IRequest<List<Tuple<string, string>>>
    {
        public string CodTabla { get; set; }
        public class GetAprobacionQueryHandler : IRequestHandler<GetAprobacionesTablaQuery, List<Tuple<string, string>>>
        {
            private readonly IApplicationDbContext _context;

            public GetAprobacionQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Tuple<string, string>>> Handle(GetAprobacionesTablaQuery request, CancellationToken cancellationToken)
            {
                var res = _context.TAprobacion.Where(t => t.CodTabla.Equals(request.CodTabla) && t.Estado).Select(a => new Tuple<string, string>(a.DocReferencia, a.EstadoDoc)).ToList();                    
                return res.OrderBy(t => t.Item2).GroupBy(x => x.Item1).Select(y => y.First()).ToList();
            }
        }
    }
}
