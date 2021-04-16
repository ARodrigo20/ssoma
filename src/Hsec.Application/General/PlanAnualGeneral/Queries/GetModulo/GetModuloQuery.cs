using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.PlanAnualGeneral.Models;

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetModulo
{
    public class GetModuloQuery : IRequest<GetModulosVM>
    {
        public class GetModuloQueryHandler : IRequestHandler<GetModuloQuery, GetModulosVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetModuloQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetModulosVM> Handle(GetModuloQuery request, CancellationToken cancellationToken)
            {
                var vm = new GetModulosVM();
                vm.list = _context.TModulo.
                    Where(t => (t.Estado==true && !t.CodModulo.Equals("01.02") && !t.CodModulo.Equals("01.03") || t.CodModulo.Equals("01.04") ) && t.CodModuloPadre != null)
                    .ProjectTo<ModuloDto>(_mapper.ConfigurationProvider)
                    .ToList();

                foreach(var mod in vm.list)
                {
                    if (mod.CodModulo.Equals("01.01"))
                    {
                        mod.Descripcion = "Obs. Tarea";
                    }
                    if (mod.CodModulo.Equals("01.05"))
                    {
                        mod.Descripcion = "Obs. (IS)";
                    }
                }

                return vm;
            }
        }
    }
}