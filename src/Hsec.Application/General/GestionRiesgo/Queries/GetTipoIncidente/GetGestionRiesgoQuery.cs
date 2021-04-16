using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Hsec.Application.Common.Interfaces;
using System.Collections.Generic;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.GestionRiesgo.Queries.GetGestionRiesgo
{
    public class GetGestionRiesgoQuery : IRequest<GestionRiesgoVM>
    {
        public class GetGestionRiesgoQueryHandler : IRequestHandler<GetGestionRiesgoQuery, GestionRiesgoVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            
            public GetGestionRiesgoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GestionRiesgoVM> Handle(GetGestionRiesgoQuery request, CancellationToken cancellationToken)
            {
                GestionRiesgoVM padre = new GestionRiesgoVM();
                var GestionRiesgoEj = _context.TGestionRiesgo.Include(i => i.Hijos);/*.AsQueryable().Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                var GestionRiesgoPadre = new GestionRiesgoVM();
                GestionRiesgoPadre.count = 0;
            
                List<TUbicacion> ListJerarquia = new List<TUbicacion>();
                var cont = 0;
                foreach (var item in GestionRiesgoEj)
                {
                    cont = item.Hijos.Count;
                }

                foreach (var item in GestionRiesgoEj)
                {
                    if (item.CodGestionRiesgoPadre == null)
                    {                  
                        GestionRiesgoNodeVM node = recursion(item);
                        GestionRiesgoPadre.data.Add(node);
                        GestionRiesgoPadre.count++;
                    }
                }               
                    return GestionRiesgoPadre;
            }

            public GestionRiesgoNodeVM recursion(TGestionRiesgo it)
            {
                var ubicacionPadre = new GestionRiesgoNodeVM();
                ubicacionPadre.Codigo = it.CodGestionRiesgo;
                ubicacionPadre.Descripcion = it.Descripcion;
                ubicacionPadre.DetalleAsociado = it.DetalleAsociado;
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
        




