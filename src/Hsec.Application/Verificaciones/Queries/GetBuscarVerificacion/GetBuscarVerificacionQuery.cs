using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Verificaciones.Models;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Hsec.Application.General.Maestro.Queries.GetMaestroData;
using Hsec.Application.General.Jerarquias.Queries.Code2Name;
using Hsec.Application.General.Personas.Queries.GetCode2NameOne;
using Hsec.Application.General.Ubicaciones.Queries.Code2Name;

namespace Hsec.Application.Verificaciones.Queries.GetBuscarVerificacion
{

    public class GetBuscarInsidentesQuery : IRequest<BuscarVerificacionVM>
    {
        public string NroVerificacion { get; set; }
        public string CodTipoVerificacion { get; set; }
        public string CodNivelRiesgo { get; set; }
        public string CodGerencia { get; set; }
        public string CodSuperIntendencia { get; set; }
        public string CodAreaHSEC { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        //public string CodPersona { get; set; }

        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }
        public class GetBuscarInsidentesQueryHandler : IRequestHandler<GetBuscarInsidentesQuery, BuscarVerificacionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IHttpContextAccessor _httpContext;

            public GetBuscarInsidentesQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
                _httpContext = HttpContext;
            }

            public async Task<BuscarVerificacionVM> Handle(GetBuscarInsidentesQuery request, CancellationToken cancellationToken)
            {
                try{
                    var vm = new BuscarVerificacionVM();

                    var userToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    var rolToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                    var filerListQuery = _context.TVerificaciones
                        .Where(v =>
                                (v.Estado == true)
                            && (String.IsNullOrEmpty(request.NroVerificacion) || v.CodVerificacion.Equals(request.NroVerificacion))

                            && (String.IsNullOrEmpty(request.CodGerencia) || v.CodPosicionGer.Equals(request.CodGerencia))
                            && (String.IsNullOrEmpty(request.CodSuperIntendencia) || v.CodPosicionSup.Equals(request.CodSuperIntendencia))
                            && (String.IsNullOrEmpty(request.CodNivelRiesgo) || v.CodNivelRiesgo.Equals(request.CodNivelRiesgo))
                            && (String.IsNullOrEmpty(request.CodTipoVerificacion) || v.CodTipoVerificacion.Equals(request.CodTipoVerificacion))
                            && (String.IsNullOrEmpty(request.CodAreaHSEC) || v.CodAreaHSEC.Equals(request.CodAreaHSEC))
                            && (String.IsNullOrEmpty(request.FechaInicio) || v.FechaVerificacion >= Convert.ToDateTime(request.FechaInicio))
                            && (String.IsNullOrEmpty(request.FechaFin) || v.FechaVerificacion <= Convert.ToDateTime(request.FechaFin))
                            && (v.CreadoPor.Contains(userToken) || rolToken != "6")
                            );
                        // .Where(v =>
                        //        (String.IsNullOrEmpty(request.CodTipoAfectado)
                        //           || (request.CodTipoAfectado.Equals(TipoAfectado.Persona) && v.TinvestigaAfectado.Count > 0))
                        //     && (String.IsNullOrEmpty(request.CodTipoAfectado)
                        //           || (request.CodTipoAfectado.Equals(TipoAfectado.Comunidad) && v.TafectadoComunidad.Count > 0))
                        //     && (String.IsNullOrEmpty(request.CodTipoAfectado)
                        //           || (request.CodTipoAfectado.Equals(TipoAfectado.Medio_Ambiente) && v.TafectadoMedioAmbiente.Count > 0))
                        //     && (String.IsNullOrEmpty(request.CodTipoAfectado)
                        //           || (request.CodTipoAfectado.Equals(TipoAfectado.Propiedad) && v.TafectadoPropiedad.Count > 0))
                        // );

                    vm.Count = filerListQuery.Count();

                    var ListQuery = filerListQuery
                        .OrderByDescending(i => i.CodVerificacion)
                        .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                        .Take(request.PaginaTamanio)
                        .ProjectTo<VerificacionBuscarDto>(_mapper.ConfigurationProvider)
                        .ToHashSet();

                    vm.Lists = ListQuery;

                    //var maestro = await _generalService.GetMaestros();
                    var maestro = await _mediator.Send(new GetMaestroDataQuery() { });

                    foreach (var item in ListQuery)
                    {
                        item.CodVerificacion = item.CodVerificacion;
                        item.Tipo = item.Tipo;
                        item.AreaHSEC = buscarPorCodigo(maestro, "AREAHSEC", item.AreaHSEC);
                        item.NivelRiesgo = buscarPorCodigo(maestro, "NIVELRIESGO", item.NivelRiesgo);
                        //item.VerificadoPor = await _generalService.GetPersonas(item.VerificadoPor);
                        item.VerificadoPor = await _mediator.Send(new GetCode2NameOneQuery() { code = item.VerificadoPor });
                        //item.Gerencia = await _generalService.GetJeraquias(item.Gerencia);
                        item.Gerencia = await _mediator.Send(new Code2NameQuery() { codigo = item.Gerencia });
                        //item.Superintendencia = await _generalService.GetJeraquias(item.Superintendencia);
                        item.Superintendencia = await _mediator.Send(new Code2NameQuery() { codigo = item.Superintendencia });
                        //item.Ubicacion = await _generalService.GetUbicaciones(item.Ubicacion);
                        item.Ubicacion = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = item.Ubicacion });
                        //item.SubUbicacion = await _generalService.GetUbicaciones(item.SubUbicacion);
                        item.SubUbicacion = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = item.SubUbicacion });
                        //item.UbicacionEspecifica = await _generalService.GetUbicaciones(item.UbicacionEspecifica);
                        item.UbicacionEspecifica = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = item.UbicacionEspecifica });
                        item.Editable = item.CreadoPor == userToken || (new string[] { "1", "4" }).Contains(rolToken);

                        if (string.IsNullOrEmpty(item.Tipo)) item.StopWork = "";
                        else if(item.Tipo.Equals(TipoVerificacion.IPERC_Continuo)){
                            var iperc = _context.TVerificacionIPERC.Where(t => t.CodVerificacion.Equals(item.CodVerificacion)).FirstOrDefault();
                            if(iperc!=null)
                                item.StopWork = buscarPorCodigo(maestro, "TOSW", iperc.StopWork);
                        }
                        else if(item.Tipo.Equals(TipoVerificacion.PTAR)){
                            var ptar = _context.TVerificacionPTAR.Where(t => t.CodVerificacion.Equals(item.CodVerificacion)).FirstOrDefault();
                            if(ptar!=null)
                                item.StopWork = buscarPorCodigo(maestro, "TOSW", ptar.StopWork);
                        }
                    }

                    vm.Lists = ListQuery;

                    return vm;
                }
                catch(Exception e){
                    throw e;
                }
            }
            private string buscarPorCodigo(ICollection<MaestroDataVM> texto, string table, string codigo)
            {

                try
                {
                    if (texto == null) return codigo;
                    var list = texto.Where(t => t.CodTabla.Equals(table)).Select(t => t.Tipos).FirstOrDefault();
                    var a = list.Where(t => t.CodRegistro.Equals(codigo)).Select(t => t.Descripcion).First();
                    return a;
                }
                catch (Exception e)
                {
                    return codigo;
                }

            }
        }
        
    }
}
