using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using Microsoft.Extensions.Configuration;

namespace Hsec.Application.General.Jerarquias.Queries.GetJerarquia
{
    public class GetJerarquiaQuery : IRequest<JerarquiaVM>
    {

        public class GetJerarquiasQueryHandler : IRequestHandler<GetJerarquiaQuery, JerarquiaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public IConfiguration Configuration { get; }
            public GetJerarquiasQueryHandler(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
            {
                _context = context;
                _mapper = mapper;
                Configuration = configuration;
            }
            public async Task<JerarquiaVM> Handle(GetJerarquiaQuery request, CancellationToken cancellationToken)
            {
                string NodoRaiz = Configuration["appSettings:NodoRaiz"];
                var jerarquiaEj = _context.TJerarquia.Where(i => i.Estado  && i.Visible && i.PathJerarquia.Substring(0, NodoRaiz.Length + 1) == "/" + NodoRaiz).Include(i => i.Hijos).AsQueryable();
                var jerarquiaPadre = new JerarquiaVM();
                jerarquiaPadre.count = 0;
                var cont = 0;
                foreach (var Jerarq in jerarquiaEj)
                {
                    if (Jerarq.Tipo == "G" && Jerarq.Estado == true) cont += Jerarq.Hijos.Count();
                }
                jerarquiaPadre.data.Add(recursion(jerarquiaEj.First(i => i.CodPosicion == int.Parse(NodoRaiz))));
                jerarquiaPadre.count = cont;
                return jerarquiaPadre;
            }

            public JerarquiaNodeVM recursion(TJerarquia it)
            {
                var jerarquiaPadre = new JerarquiaNodeVM();
                jerarquiaPadre.data = it.CodPosicion;
                jerarquiaPadre.label = it.Descripcion;
                jerarquiaPadre.Tipo = it.Tipo;
                jerarquiaPadre.CodElipse = it.CodElipse;
                if (it.Hijos.Count == 0)
                {
                    return jerarquiaPadre;
                }


                if (it.Hijos.Count > 0)
                {
                    foreach (var hijo in it.Hijos)
                    {

                        if (hijo.Tipo != "O" && hijo.Estado && hijo.Visible)
                        {
                            jerarquiaPadre.children.Add(recursion(hijo));
                        }
                    }
                }

                return jerarquiaPadre;
            }
        }
    }
}





