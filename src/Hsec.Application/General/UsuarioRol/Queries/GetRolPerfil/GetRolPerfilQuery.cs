using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Hsec.Application.General.Roles.Queries.GetRolPerfil
{
    public class GetRolPerfilQuery : IRequest<GetRolPerfilVM>
    {
        public string CodRol {get;set;}

        public class GetRolPerfilHandler : IRequestHandler<GetRolPerfilQuery, GetRolPerfilVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public GetRolPerfilHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = HttpContext;
            }

            public async Task<GetRolPerfilVM> Handle(GetRolPerfilQuery request, CancellationToken cancellationToken)
            {
                var codRol = Int32.Parse(request.CodRol);
                var rol = _context.TRol.Where(t => t.Estado == true && t.CodRol == codRol).FirstOrDefault();

                if(rol==null) throw new Exception("no existe rol");

                var vm = new GetRolPerfilVM();

                vm.Descripcion = rol.Descripcion;
                vm.CodRol = codRol.ToString();

                var RolAcceso = _context.TRolAcceso.Where(t => t.Estado == true && t.CodRol == codRol).Select(t => t.CodAcceso).ToList();
                int[] roles = {codRol};

                var Accesos = _context.TAcceso.Where(t => RolAcceso.Contains(t.CodAcceso))
                    .ProjectTo<AccesosDto>(_mapper.ConfigurationProvider).ToList();
                // var Accesos = _context.TAcceso.Include(i => i.Hijos).Include(i => i.RolAccesos).Where(a => a.RolAccesos.Where(t => t.CodRol==codRol)).ToList();
                // var AccesosUser = Accesos.Where(a => a.RolAccesos.Where(x => x.Estado).Select(x => x.CodRol).Intersect(roles).Any()).ToList();

                vm.Permisos = Accesos;
                // rol.RolAccesos = _context.TRolAcceso.Where(t => t.Estado == true && t.CodRol.Equals(request.CodRol))
                return vm;
            }


        }
    }
}