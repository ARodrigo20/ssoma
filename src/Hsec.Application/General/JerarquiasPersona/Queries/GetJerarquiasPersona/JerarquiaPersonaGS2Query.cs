using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using Hsec.Application.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Hsec.Application.General.Jerarquias.Queries.GetJerarquia;

namespace Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiasPersona
{
    public class JerarquiaPersonaGS2Query : IRequest<JerarquiaVM>
    {
        public string codPersona { get; set; }

        public class JerarquiaPersonaGS2QueryHandler : IRequestHandler<JerarquiaPersonaGS2Query, JerarquiaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public IConfiguration Configuration { get; }
            public JerarquiaPersonaGS2QueryHandler(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
            {
                _context = context;
                _mapper = mapper;
                Configuration = configuration;
            }
            public bool estadoS = true;
            public bool estadoG = true;
            public JerarquiaVM nodos;
            public async Task<JerarquiaVM> Handle(JerarquiaPersonaGS2Query request, CancellationToken cancellationToken)
            {              
                string codPersona = request.codPersona;
                var jerarquiaPersona = await _context.TJerarquiaPersona.FirstOrDefaultAsync(i => i.CodPersona == codPersona);

                if (jerarquiaPersona == null) throw new NotFoundException("Jerarquia Persona",codPersona);

                var jerarquiaEj = _context.TJerarquia.Where(i => i.CodPosicion == jerarquiaPersona.CodPosicion).Include(i => i.Padre).First() ;

                JerarquiaNodeVM jerarquiaPadre = new JerarquiaNodeVM();
                jerarquiaPadre.data = jerarquiaEj.CodPosicion;
                jerarquiaPadre.label = jerarquiaEj.Descripcion;
                jerarquiaPadre.Tipo = jerarquiaEj.Tipo;
                jerarquiaPadre.CodElipse = jerarquiaEj.CodElipse;

                nodos = new JerarquiaVM();
                nodos.data.Add(jerarquiaPadre);
                //if (Jerarquia.Tipo == "O" || Jerarquia.Tipo == "S" || Jerarquia.Tipo == "G" && Jerarquia.Estado == true) {
                if (jerarquiaEj.Tipo != "G")
                {
                    recursion(jerarquiaEj);
                }
                nodos.count = nodos.data.Count;
                return nodos;
            }
            public void recursion(TJerarquia nodo)
            {
                JerarquiaNodeVM jerarquiaPadre = new JerarquiaNodeVM();
                jerarquiaPadre.data = nodo.CodPosicion;
                jerarquiaPadre.label = nodo.Descripcion;
                jerarquiaPadre.Tipo = nodo.Tipo;
                jerarquiaPadre.CodElipse = nodo.CodElipse;
                TJerarquia padre = _context.TJerarquia.Where(i => i.CodPosicion == nodo.CodPosicionPadre).Include(i => i.Padre).First();
                     
                if (nodo.Tipo != "G") {                        

                        if (nodo.Tipo == "S" && estadoS)
                        {
                            estadoS = false;
                            nodos.data.Add(jerarquiaPadre);
                        }                                          
                }
                else{
                    if (nodo.Tipo == "G" && estadoG)
                    {
                        estadoG = false;
                        nodos.data.Add(jerarquiaPadre);
                        return;
                    }                                
                }
                recursion(padre);
                
            }
        }
    }
}
