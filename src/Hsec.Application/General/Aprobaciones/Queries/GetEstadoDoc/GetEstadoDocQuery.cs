
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Hsec.Application.General.Aprobaciones.Queries.GetEstadoDoc
{
    public class GetEstadoDocQuery : IRequest<string>
    {
        public string docReferencia { get; set; }
        public string codTabla { get; set; }

        public class GetEstadoDocQueryHandler : IRequestHandler<GetEstadoDocQuery, string>
        {
            private readonly IApplicationDbContext _context;

            public GetEstadoDocQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(GetEstadoDocQuery request, CancellationToken cancellationToken)
            {
                var reg = _context.TAprobacion.Where(t => t.DocReferencia == request.docReferencia && t.CodTabla == request.codTabla).OrderByDescending(d => d.Version).FirstOrDefault();
                if (reg == null) return "N";
                else if (reg.EstadoDoc == null) return "";
                else return reg.EstadoDoc;
            }
        }
    }
}
