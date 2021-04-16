using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Hsec.Application.General.Aprobaciones.Queries.GetDocAprobacion
{
    public class GetDocAprobacionQuery : IRequest<GetDocAprobacionVM>
    {
        public class GetDocAprobacionQueryHandler : IRequestHandler<GetDocAprobacionQuery, GetDocAprobacionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public GetDocAprobacionQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = HttpContext;
            }

            private const string ADMIN = "admin";

            private string peek(string cadenaAprobacion)
            {
                var list = cadenaAprobacion.Split('.');
                var apro = list.LastOrDefault();
                return apro;
            }
            // private const int PENDITENTE = null;
            public async Task<GetDocAprobacionVM> Handle(GetDocAprobacionQuery request, CancellationToken cancellationToken)
            {
                var vm = new GetDocAprobacionVM();
                var codPersona = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var user = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (!user.Equals(ADMIN)){

                    var tjer = _context.TJerarquiaPersona.Where(t => t.CodPersona.Equals(codPersona) && t.Estado == true).FirstOrDefault();
                    if (tjer == null) return vm;
                    var codPosicion = tjer.CodPosicion;

                    vm.list = _context.TAprobacion.Where(t => t.EstadoDoc == null && t.Estado == true && (EF.Functions.Like(t.CadenaAprobacion, "%" + codPosicion) || EF.Functions.Like(t.CadenaAprobacion, "%" + codPersona)))
                        .Select(l => new DocAprobacionDto() {CodTabla= l.CodTabla, DocReferencia= l.DocReferencia }).ToList();                                                       
                }
                else{
                    vm.list = _context.TAprobacion.Where(t => t.EstadoDoc == null && t.Estado==true).Select(l => new DocAprobacionDto() { CodTabla = l.CodTabla, DocReferencia = l.DocReferencia }).ToList();
                }
                vm.list.Reverse();
                return vm;
            }
        }
    }
}