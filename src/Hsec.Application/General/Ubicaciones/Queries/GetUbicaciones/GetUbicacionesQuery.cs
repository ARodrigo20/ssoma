using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Hsec.Application.Common.Interfaces;
using System.Collections.Generic;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Ubicaciones.Queries.GetUbicaciones
{
    public class GetUbicacionesQuery : IRequest<UbicacionesVM>
    {
        public class GetUbicacionesQueryHandler : IRequestHandler<GetUbicacionesQuery, UbicacionesVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
                               
            public GetUbicacionesQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UbicacionesVM> Handle(GetUbicacionesQuery request, CancellationToken cancellationToken)
            {
                //var ubicacionesEj = _context.TUbicacion.Include(i => i.Hijos);/*.AsQueryable().Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                //var ubicacionesPadre = new UbicacionesVM();
                //ubicacionesPadre.count = 0;

                //List<TUbicacione> ListJerarquia = new List<TUbicacione>();
                //int cont = 0;
                //foreach (var ubi in ubicacionesEj)
                //{
                //    cont += ubi.Hijos.Count();

                //}

                //foreach (var item in ubicacionesEj)
                //{
                //    if (item.CodUbicacionPadre == null)
                //    {                  
                //        UbicacionesNodeVM node = recursion(item);
                //        ubicacionesPadre.data.Add(node);
                //        ubicacionesPadre.count++;
                //    }
                //}
                //    return ubicacionesPadre;


                ///-------------------------------------
                ///
                var ubicaciones_db = _context.TUbicacion
                    .Where(t => t.Estado == true)
                    .ToHashSet();

                var ubicacionesEj = ubicaciones_db
                    .Where(t => t.CodUbicacionPadre == null)
                    .ToHashSet();

                var ubicacionesPadre = new UbicacionesVM();
                ubicacionesPadre.count = 0;

                List<TUbicacion> ListJerarquia = new List<TUbicacion>();
                int cont = 0;

                foreach (var item in ubicacionesEj)
                {
                    UbicacionesNodeVM node = _recursion(item, ubicaciones_db);
                    ubicacionesPadre.data.Add(node);
                    ubicacionesPadre.count++;
                }

                return ubicacionesPadre;
            }

            //public UbicacionesNodeVM recursion(TUbicacione it)
            //{
            //    var ubicacionPadre = new UbicacionesNodeVM();
            //    ubicacionPadre.data = it.CodUbicacion;
            //    ubicacionPadre.label = it.Descripcion;
            //    if (it.Hijos.Count == 0)
            //    {
            //        return ubicacionPadre;
            //    }

                
            //    if (it.Hijos.Count > 0)
            //    {
            //        foreach (var hijos in it.Hijos)
            //        {                      
            //            ubicacionPadre.children.Add(recursion(hijos));
            //        }
            //    }
            //    return ubicacionPadre;
            //}

            public UbicacionesNodeVM _recursion(TUbicacion it, HashSet<TUbicacion> _ubicaciones_db)
            {
                var ubicacionPadre = new UbicacionesNodeVM();
                ubicacionPadre.data = it.CodUbicacion;
                ubicacionPadre.label = it.Descripcion;
                //it.Hijos = _context.TUbicacion.Where(t => t.CodUbicacionPadre.Equals(it.CodUbicacion)).ToHashSet();
                it.Hijos = _ubicaciones_db.Where(t => t.CodUbicacionPadre != null && t.CodUbicacionPadre.Equals(it.CodUbicacion)).ToHashSet();

                if (it.Hijos.Count > 0 && nobuqules(it.CodUbicacion, it.CodUbicacionPadre))
                {
                    foreach (var hijo in it.Hijos)
                    {
                        ubicacionPadre.children.Add(_recursion(hijo, _ubicaciones_db));
                    }
                }
                return ubicacionPadre;
            }

            private bool nobuqules(string CodUbicacion, string CodUbicacionPadre)
            {
                var tam = CodUbicacion == null ? 0 : CodUbicacion.Split('.').Count();
                var tamPadre = CodUbicacionPadre == null ? 0 : CodUbicacionPadre.Split('.').Count();
                return tam > tamPadre;
            }
        }
    }
}
        




