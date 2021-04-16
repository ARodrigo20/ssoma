using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Hsec.Application.General.Roles.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<RolVM>
    {

        public class GetRolesHandler : IRequestHandler<GetRolesQuery, RolVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public GetRolesHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = HttpContext;
            }

            public async Task<RolVM> Handle(GetRolesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var list = _context.TRol
                        .Where(t => t.Estado == true )
                        .Select(t => new RolDto(){
                            CodRol = t.CodRol.ToString(),
                            Descripcion = t.Descripcion
                        })
                        .ToList();

                    var vm = new RolVM();

                    vm.list = list;
                    vm.Count = list.Count;

                    return vm;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}