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
using Hsec.Application.General.Jerarquias.Queries.Code2Name;

namespace Hsec.Application.Comite.Queries.ComiteSearch
{
    public class ComiteSearchQuery : IRequest<ComiteVM>
    {
        public string anio { get; set; }

        public string mes { get; set; }

        public string CodTipoComite { get; set; }

        public string CodCategoria { get; set; }

        public string CodPosicionGer { get; set; }

        public string CodPosicionSup { get; set; }

        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }

        public class ComiteSearchHandler : IRequestHandler<ComiteSearchQuery, ComiteVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IHttpContextAccessor _httpContext;

            public ComiteSearchHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
                _httpContext = HttpContext;
            }

            public async Task<ComiteVM> Handle(ComiteSearchQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var vm = new ComiteVM();

                    var userToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    var rolToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                    var filerListQuery = _context.TComite
                        .Where(t => (t.Fecha.Year.ToString() == request.anio || string.IsNullOrEmpty(request.anio))
                            && (t.Fecha.Month.ToString() == request.mes || string.IsNullOrEmpty(request.mes))
                            && (t.CodTipoComite == request.CodTipoComite || string.IsNullOrEmpty(request.CodTipoComite))
                            && (t.CodCategoria == request.CodCategoria || string.IsNullOrEmpty(request.CodCategoria))
                            && (t.CodPosicionGer == request.CodPosicionGer || string.IsNullOrEmpty(request.CodPosicionGer))
                            && (t.CodPosicionSup == request.CodPosicionSup || string.IsNullOrEmpty(request.CodPosicionSup))
                            && t.Estado == true && (t.CreadoPor.Contains(userToken) || rolToken != "6"));

                    vm.count = filerListQuery.Count();

                    var ListQuery = filerListQuery
                            .OrderByDescending(t => t.CodComite)
                            .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                            .Take(request.PaginaTamanio)
                            .ProjectTo<ComiteSearchDto>(_mapper.ConfigurationProvider)
                            .ToHashSet();

                    vm.Lists = ListQuery;

                    foreach (var row in vm.Lists)
                    {
                        row.Editable = row.UsuCreacion == userToken || (new string[] { "1", "4" }).Contains(rolToken);

                        //row.CodPosicionGer = await _generalService.GetJeraquias(row.CodPosicionGer);
                        row.CodPosicionGer = await _mediator.Send(new Code2NameQuery() { codigo = row.CodPosicionGer });

                        //row.CodPosicionSup = await _generalService.GetJeraquias(row.CodPosicionSup);
                        row.CodPosicionSup = await _mediator.Send(new Code2NameQuery() { codigo = row.CodPosicionSup });
                    }

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