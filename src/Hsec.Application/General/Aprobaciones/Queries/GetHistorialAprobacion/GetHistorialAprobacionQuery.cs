using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Queries.GetHistorialAprobacion
{
    public class GetHistorialAprobacionQuery : IRequest<GetHistorialAprobacionVM>
    {
        public string DocReferencia { get; set; }
        public class GetHistorialAprobacionQueryHandler : IRequestHandler<GetHistorialAprobacionQuery, GetHistorialAprobacionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IHttpContextAccessor _httpContext;

            public GetHistorialAprobacionQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContext)
            {
                _context = context;
                _httpContext = httpContext;
            }

            public async Task<GetHistorialAprobacionVM> Handle(GetHistorialAprobacionQuery request, CancellationToken cancellationToken)
            {       
                var aprobacionEnd = _context.TAprobacion.Where(t => t.DocReferencia.Equals(request.DocReferencia)).OrderByDescending(t => t.Creado).FirstOrDefault();
                var vm = new GetHistorialAprobacionVM();

                var Historial = _context.TAprobacion.Where( t => t.DocReferencia.Equals(request.DocReferencia))
                    .Join( _context.THistorialAprobacion,
                        aprobacion => aprobacion.CodAprobacion,
                        historialAprobacion => historialAprobacion.CodAprobacion,
                        (aprobacion, historialAprobacion) => new HAprobacionDto()
                        {
                            DocReferencia = request.DocReferencia,
                            CodAprobacion = aprobacion.CodAprobacion,
                            Version = aprobacion.Version,
                            CodAprobador = historialAprobacion.CodAprobador,
                            Comentario = historialAprobacion.Comentario,
                            EstadoAprobacion = historialAprobacion.EstadoAprobacion,
                            FechaCreacion = historialAprobacion.Creado
                        }
                    ).ToList();

                foreach (var item in aprobacionEnd.CadenaAprobacion.Split(".").Reverse()) // agregamos al historia las aprobaciones pendientes
                {
                    var hist = new HAprobacionDto();
                    var dataitem = item.Split("-");
                    var AprobadorCod = "";

                    if (dataitem[0] == "J")
                    {
                        AprobadorCod = string.Join(",",_context.TJerarquiaPersona.Where(j => j.CodPosicion == Convert.ToInt32(dataitem[1]) && j.CodTipoPersona== 1 && j.FechaInicio < DateTime.Now && j.FechaFin> DateTime.Now).Select(p => p.CodPersona));                                                                
                    }
                    else AprobadorCod = dataitem[1];

                    var RolUser = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value; // 

                    Historial.Add(new HAprobacionDto {
                    DocReferencia = request.DocReferencia,
                        CodAprobacion = aprobacionEnd.CodAprobacion,
                        Version = aprobacionEnd.Version,
                        CodCadena = item,
                        Editable= RolUser!=null?RolUser.Equals("1") || RolUser.Equals("4"):false, // editable para administardores y usuarios HSEC
                        CodAprobador = AprobadorCod
                    });
                }

                foreach (var item in Historial)
                {
                    if (!String.IsNullOrEmpty(item.CodAprobador)) {
                        var dataAprobador = item.CodAprobador.Split(",");
                        var Aprobadores = _context.TPersona.Where(t => dataAprobador.Contains(t.CodPersona));
                        item.Aprobador = string.Join(",", Aprobadores.Select(p => p.ApellidoPaterno + ' ' + p.ApellidoMaterno + ',' + p.Nombres));
                        item.Email = string.Join(",", Aprobadores.Select(p => p.Email));
                        item.Cargo = _context.TJerarquiaPersona.Join(_context.TJerarquia, jer => jer.CodPosicion, per => per.CodPosicion, (jer, per) => new { jer = jer, per = per })
                        .Where(tuple => (tuple.jer.CodPersona == dataAprobador[0] && tuple.jer.CodTipoPersona == 1)).Select(t => t.per.Descripcion).FirstOrDefault();
                    }                    
                }
                // load cadena

                
                vm.Lista = Historial;
                return vm;
            }
        }
    }
}