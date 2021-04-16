
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Hsec.Application.General.Aprobaciones.Queries.GetEstadoDoc
{
    public class GetEstadoListDocQuery : IRequest<List<Tuple<string, string>>>
    {
        public List<string> docsReferencia { get; set; }
        public string codTabla { get; set; }
        public string estadoDoc { get; set; }

        public class GetEstadoListDocQueryHandler : IRequestHandler<GetEstadoListDocQuery, List<Tuple<string, string>>>
        {
            private readonly IApplicationDbContext _context;

            public GetEstadoListDocQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Tuple<string, string>>> Handle(GetEstadoListDocQuery request, CancellationToken cancellationToken)
            {
                return  _context.TAprobacion.Where(t => request.docsReferencia.Contains(t.DocReferencia) && t.CodTabla == request.codTabla && (t.EstadoDoc == request.estadoDoc || string.IsNullOrEmpty(request.estadoDoc))).Select(t => new Tuple<string,string>(t.DocReferencia , t.EstadoDoc)).ToList();                
            }
        }
    }
}
