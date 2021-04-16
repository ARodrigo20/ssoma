using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Hsec.Application.Common.Interfaces;
using System.Collections.Generic;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.TipoIncidente.Queries.GetTipoIncidente
{
    public class GetTipoIncidenteQuery : IRequest<TipoIncidenteVM>
    {
        public class GetTipoIncidenteQueryHandler : IRequestHandler<GetTipoIncidenteQuery, TipoIncidenteVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
                               
            public GetTipoIncidenteQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<TipoIncidenteVM> Handle(GetTipoIncidenteQuery request, CancellationToken cancellationToken)
            {
                TipoIncidenteVM padre = new TipoIncidenteVM();
                var TipoIncidenteEj = _context.TTipoIncidente.Include(i => i.Hijos);/*.AsQueryable().Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                var TipoIncidentePadre = new TipoIncidenteVM();
                TipoIncidentePadre.count = 0;
               
                List<TUbicacion> ListJerarquia = new List<TUbicacion>();
                var cont = 0;
                foreach (var item in TipoIncidenteEj)
                {
                    cont = item.Hijos.Count;
                }

                foreach (var item in TipoIncidenteEj)
                {
                    if (item.CodPadreTipoIncidente == null)
                    {                  
                        TipoIncidenteNodeVM node = recursion(item);
                        TipoIncidentePadre.data.Add(node);
                        TipoIncidentePadre.count++;
                    }
                }               
                    return TipoIncidentePadre;
            }

            public TipoIncidenteNodeVM recursion(TTipoIncidente it)
            {
                var ubicacionPadre = new TipoIncidenteNodeVM();
                ubicacionPadre.data = it.CodTipoIncidente;
                ubicacionPadre.label = it.Descripcion;
                if (it.Hijos.Count == 0)
                {
                    return ubicacionPadre;
                }

                
                if (it.Hijos.Count > 0)
                {
                    foreach (var hijos in it.Hijos)
                    {                      
                        ubicacionPadre.children.Add(recursion(hijos));
                    }
                }
                return ubicacionPadre;
            }
        }
    }
}
        




