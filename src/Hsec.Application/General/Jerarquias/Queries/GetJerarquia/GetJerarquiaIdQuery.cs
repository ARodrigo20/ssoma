using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Hsec.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Hsec.Application.General.Jerarquias.Queries.GetJerarquia
{
    public class GetJerarquiaIdQuery : IRequest<JerarquiaNodeVM>
    {
        public int CodPosicion { get; set; }
        public class GetJerarquiaIdQueryHandler : IRequestHandler<GetJerarquiaIdQuery, JerarquiaNodeVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public IConfiguration Configuration { get; }
            public GetJerarquiaIdQueryHandler(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
            {
                _context = context;
                _mapper = mapper;
                Configuration = configuration;
            }
            public async Task<JerarquiaNodeVM> Handle(GetJerarquiaIdQuery request, CancellationToken cancellationToken)
            {
                var jerarquiaPadre = new JerarquiaNodeVM();

                var jerarquiaEj = _context.TJerarquia.Where(i => i.Estado && i.Visible && i.CodPosicion == request.CodPosicion).Include(i => i.Hijos).FirstOrDefault();
                if (jerarquiaEj != null) {
                    jerarquiaPadre.data = jerarquiaEj.CodPosicion;
                    jerarquiaPadre.label = jerarquiaEj.Descripcion;
                    jerarquiaPadre.Tipo = jerarquiaEj.Tipo;
                    jerarquiaPadre.CodElipse = jerarquiaEj.CodElipse;

                    foreach (var it in jerarquiaEj.Hijos) {
                        var hijo = new JerarquiaNodeVM();
                        hijo.data = it.CodPosicion;
                        hijo.label = it.Descripcion;
                        hijo.Tipo = it.Tipo;
                        hijo.CodElipse = it.CodElipse;
                        jerarquiaPadre.children.Add(hijo);
                    }
                }
                return jerarquiaPadre;
            }

        }
    }
}
