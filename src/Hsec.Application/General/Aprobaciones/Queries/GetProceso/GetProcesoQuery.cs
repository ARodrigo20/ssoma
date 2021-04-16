using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Queries.GetProceso
{
    public class GetProcesoQuery : IRequest<GetProcesoVM>
    {
        public string CodProceso { get; set; }
        public class GetProcesoQueryHandler : IRequestHandler<GetProcesoQuery, GetProcesoVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetProcesoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetProcesoVM> Handle(GetProcesoQuery request, CancellationToken cancellationToken)
            {
                var obj = _context.TProceso.Find(request.CodProceso);
                var vm = _mapper.Map<GetProcesoVM>(obj);
                var lista = obj.CadenaAprobacion.Split('.').Select(t => new GetCadenaAprobacionDto(){CodCadenaAprobacion=t}).ToList();
                
                foreach (var item in lista)
                {
                    var CadenaAprobacion = _context.TMaestro.Where(t => t.CodTipo.Equals(item.CodCadenaAprobacion) && t.Estado==true ).FirstOrDefault();
                    item.CadenaAprobacion = CadenaAprobacion == null? "" : CadenaAprobacion.Descripcion;
                }
                vm.Lista = lista;
                return vm;
            }
        }
    }
}