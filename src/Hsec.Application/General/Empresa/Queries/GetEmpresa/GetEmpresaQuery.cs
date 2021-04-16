using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Empresa.Queries.GetEmpresa
{

    public class GetEmpresaQuery : IRequest<ListEmpresaVM>
    {

        public class GetEmpresaQueryHandler : IRequestHandler<GetEmpresaQuery, ListEmpresaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetEmpresaQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ListEmpresaVM> Handle(GetEmpresaQuery request, CancellationToken cancellationToken)
            {
                ListEmpresaVM vm = new ListEmpresaVM();
                vm.data = _context.TProveedor.ProjectTo<EmpresaDto>(_mapper.ConfigurationProvider).ToList();
                return vm;
            }
        }

       
    }
}