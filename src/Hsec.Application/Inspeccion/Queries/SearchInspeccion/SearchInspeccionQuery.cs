using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Hsec.Application.General.Jerarquias.Queries.Code2Name;
using Hsec.Application.General.Ubicaciones.Queries.Code2Name;
using Hsec.Application.General.Empresa.Queries.GetCode2Name;

namespace Hsec.Application.Inspeccion.Queries.SearchInspeccion
{
    public class SearchInspeccionQuery : IRequest<SearchInspeccionVM>
    {
        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }
        public string NroInspeccion { get; set; }
        public string Ubicacion { get; set; }
        public string SubUbicacion { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public string Gerencia { get; set; }
        public string SuperIntendencia { get; set; }
        public string Contrata { get; set; }
        public string CodPersona { get; set; }
        public string CodTipo { get; set; }
        public string AreaAlcance { get; set; }

        public class SearchInspecciomQueryHandler : IRequestHandler<SearchInspeccionQuery, SearchInspeccionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IHttpContextAccessor _httpContext;

            public SearchInspecciomQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
                _httpContext = HttpContext;
            }

            public async Task<SearchInspeccionVM> Handle(SearchInspeccionQuery request, CancellationToken cancellationToken)
            {
                SearchInspeccionVM VM = new SearchInspeccionVM();

                var userToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var rolToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                var filerListQuery = (from inspeccion in _context.TInspeccion
                                      join equipoInspeccion in _context.TEquipoInspeccion on inspeccion.CodInspeccion equals equipoInspeccion.CodInspeccion into lista
                                      from list in lista.DefaultIfEmpty()
                                      where
                                            inspeccion.Estado == true &&
                                            (string.IsNullOrEmpty(request.NroInspeccion) || inspeccion.CodInspeccion.Contains(request.NroInspeccion)) &&
                                            (string.IsNullOrEmpty(request.Ubicacion) || inspeccion.CodUbicacion.Equals(request.Ubicacion)) &&
                                            (string.IsNullOrEmpty(request.SubUbicacion) || inspeccion.CodSubUbicacion.Equals(request.SubUbicacion)) &&
                                            (string.IsNullOrEmpty(request.Anio.ToString()) || inspeccion.Fecha.Value.Year == Int32.Parse(request.Anio)) &&
                                            (string.IsNullOrEmpty(request.Mes.ToString()) || inspeccion.Fecha.Value.Month == Int32.Parse(request.Mes)) &&
                                            (string.IsNullOrEmpty(request.Gerencia) || inspeccion.Gerencia.Equals(request.Gerencia)) &&
                                            (string.IsNullOrEmpty(request.SuperIntendencia) || inspeccion.SuperInt.Equals(request.SuperIntendencia)) &&
                                            (string.IsNullOrEmpty(request.Contrata) || inspeccion.CodContrata.Equals(request.Contrata)) &&
                                            (string.IsNullOrEmpty(request.CodTipo) || inspeccion.CodTipo.Equals(request.CodTipo)) &&
                                            (string.IsNullOrEmpty(request.AreaAlcance) || inspeccion.AreaAlcance.Equals(request.AreaAlcance)) &&
                                            (string.IsNullOrEmpty(request.CodPersona) || list.CodPersona.Equals(request.CodPersona)) &&
                                            (inspeccion.CreadoPor.Contains(userToken) || rolToken != "6")
                                      select new SearchInspeccionDto
                                      {
                                          CodInspeccion = inspeccion.CodInspeccion,
                                          CodUbicacion = inspeccion.CodUbicacion,
                                          CodSubUbicacion = inspeccion.CodSubUbicacion,
                                          Gerencia = inspeccion.Gerencia,
                                          SuperInt = inspeccion.SuperInt,
                                          CodContrata = inspeccion.CodContrata,
                                          Fecha = inspeccion.Fecha,
                                          FechaP = inspeccion.FechaP,
                                          Hora = inspeccion.Hora,
                                          CodTipo=inspeccion.CodTipo,
                                          AreaAlcance=inspeccion.AreaAlcance

                                      }).ToList().GroupBy(i => i.CodInspeccion).Select(o => o.First());

                VM.size = filerListQuery.Count();

                var ListQuery = filerListQuery
                    .OrderByDescending(i => i.CodInspeccion)
                    .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                    .Take(request.PaginaTamanio)
                    .ToHashSet();

                VM.data = ListQuery;

                foreach (var row in VM.data)
                {
                    // row.DescripcionGer = await _jerarquia.GetDescripcion(row.Gerencia);
                    row.DescripcionGer = await _mediator.Send(new Code2NameQuery() { codigo = row.Gerencia });
                    if (!string.IsNullOrEmpty(row.SuperInt))
                    {
                        //row.DescripcionSuperint = await _jerarquia.GetDescripcion(row.SuperInt);
                        row.DescripcionSuperint = await _mediator.Send(new Code2NameQuery() { codigo = row.SuperInt });
                    }
                    if (!string.IsNullOrEmpty(row.CodUbicacion))
                    {
                        //row.DescripcionUbicacion = await _ubicacion.GetDescripcion(row.CodUbicacion);
                        row.DescripcionUbicacion = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = row.CodUbicacion });
                    }
                    if (!string.IsNullOrEmpty(row.CodSubUbicacion))
                    {
                        //row.DescripcionSubUbicacion = await _ubicacion.GetDescripcion(row.CodSubUbicacion);
                        row.DescripcionSubUbicacion = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = row.CodSubUbicacion });
                    }
                    if (!string.IsNullOrEmpty(row.CodContrata))
                    {
                        //row.DescripcionContrata = await _proveedor.GetDescripcion(row.CodContrata);
                        row.DescripcionContrata = await _mediator.Send(new GetCode2NameQuery() { codigo = row.CodContrata });
                    }
                    row.Editable = row.UsuCreacion == userToken || (new string[] { "1", "4" }).Contains(rolToken);
                }

                return VM;
            }
        }

    }
}