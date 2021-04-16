using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Hsec.Application.Common.Interfaces;
using System.Collections.Generic;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Modulos.Queries.GetModulo
{
    public class GetModuloQuery : IRequest<ModuloVM>
    {
        public class GetModuloQueryHandler : IRequestHandler<GetModuloQuery, ModuloVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetModuloQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ModuloVM> Handle(GetModuloQuery request, CancellationToken cancellationToken)
            {
                var ModuloEj = _context.TModulo.Include(i => i.Hijos);/*.AsQueryable().Distinct().OrderBy(x => x);*//*.Skip(0).Take(10);*/

                var ModuloPadre = new ModuloVM();
                ModuloPadre.count = 0;

                List<TModulo> ListJerarquia = new List<TModulo>();
                int cont = 0;
                foreach (var ubi in ModuloEj)
                {
                    cont += ubi.Hijos.Count();

                }

                foreach (var item in ModuloEj)
                {
                    if (item.CodModuloPadre == null)
                    {                  
                        ModuloNodeVM node = recursion(item);
                        ModuloPadre.data.Add(node);
                        ModuloPadre.count++;
                    }
                }
                    return ModuloPadre;
            }

            public ModuloNodeVM recursion(TModulo it)
            {
                var ModuloPadre = new ModuloNodeVM();
                ModuloPadre.data = it.CodModulo;
                ModuloPadre.label = it.Descripcion;
                if (it.Hijos.Count == 0)
                {
                    return ModuloPadre;
                }

                
                if (it.Hijos.Count > 0)
                {
                    foreach (var hijos in it.Hijos)
                    {                      
                        ModuloPadre.children.Add(recursion(hijos));
                    }
                }
                return ModuloPadre;
            }
        }
    }
}
        




