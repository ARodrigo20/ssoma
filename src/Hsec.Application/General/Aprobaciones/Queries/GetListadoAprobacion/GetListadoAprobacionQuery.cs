using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Queries.GetListadoAprobacion
{
    public class GetListadoAprobacionQuery : IRequest<bool>
    {
        public string DocReferencia { get; set; }
        public class GetHistorialAprobacionQueryHandler : IRequestHandler<GetListadoAprobacionQuery, bool>
        {
            private readonly IApplicationDbContext _context;
            private readonly IHttpContextAccessor _httpContext;

            public GetHistorialAprobacionQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContext)
            {
                _context = context;
                _httpContext = httpContext;
            }

            public async Task<bool> Handle(GetListadoAprobacionQuery request, CancellationToken cancellationToken)
            {
                var aprobaciones = _context.TAprobacionPlan.Where(a => a.DocReferencia.Equals(request.DocReferencia)).ToList();

                //var _aprobaciones = new List<string>();

                //foreach(var item in aprobaciones)
                //{
                //    if (item.EstadoDoc.Equals("M") || item.EstadoDoc.Equals("R") || item.EstadoDoc.Equals("N") || item.EstadoDoc.Equals("P")|| item.EstadoDoc == null) return false;
                //}

                var resp = true;
                if (aprobaciones.Count() == 0)
                {
                    resp = false;
                }
                else if (aprobaciones.Count() > 0)
                {
                    foreach (var item in aprobaciones)
                    {
                        if (item.EstadoDoc.Equals("P") || item.EstadoDoc.Equals("R") || item.EstadoDoc == null)
                        {
                            resp = false;
                        }
                    }
                }

                return resp;
            }
        }
    }
}
