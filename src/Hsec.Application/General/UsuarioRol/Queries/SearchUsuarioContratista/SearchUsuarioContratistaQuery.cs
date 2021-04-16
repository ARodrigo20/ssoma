using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Collections.Generic;
using Hsec.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hsec.Application.General.UsuarioRol.Queries.SearchUsuario
{
    public class SearchUsuarioContratistaQuery : IRequest<UsuarioVM>
    {
        public string CodUsuario { get; set; }

        public string Usuario { get; set; }

        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }

        public class SearchUsuarioQueryHandler : IRequestHandler<SearchUsuarioContratistaQuery, UsuarioVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public SearchUsuarioQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = HttpContext;
            }

            public async Task<UsuarioVM> Handle(SearchUsuarioContratistaQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var vm = new UsuarioVM();

                    var filerListQuery = (from usr in _context.TUsuario
                                 join usuRol in _context.TUsuarioRol on usr.CodUsuario equals usuRol.CodUsuario
                                 where (
                                    usr.TipoLogueo == false
                                    && usr.Estado == true 
                                    && usuRol.CodRol == 6 
                                    && (request.Usuario == null || usr.Usuario.Contains(request.Usuario))
                                    && (request.CodUsuario == null || usr.CodUsuario.ToString().Contains(request.CodUsuario) )
                                 )
                                orderby usr.CodUsuario
                                 select new SearchUsuarioContratistaDto()
                                 {
                                     CodUsuario = usr.CodUsuario,
                                     Usuario = usr.Usuario
                                 }
                                 );

                    vm.Count = filerListQuery.Count();

                    var ListQuery = filerListQuery
                            .OrderByDescending(t => t.CodUsuario)
                            .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                            .Take(request.PaginaTamanio)
                            .ToHashSet();

                    vm.List = ListQuery;
                    vm.Pagina = request.Pagina;


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
