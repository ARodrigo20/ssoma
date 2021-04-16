using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Models;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Hsec.Application.General.Maestro.Queries.GetMaestroData;
using Hsec.Application.General.Ubicaciones.Queries.Code2Name;
using Hsec.Application.General.Jerarquias.Queries.Code2Name;
using Hsec.Application.General.Personas.Queries.GetCode2NameOne;

namespace Hsec.Application.Observacion.Queries.GetObservacionesBuscar
{
    public class GetObservacionesBuscarQuery : IRequest<ObservacionesVM>
    {
        public string NroObservacion { get; set; }
        public string CodTipoObservacion { get; set; }
        public string CodNivelRiesgo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string CodGerencia { get; set; }
        public string CodSuperIntendencia { get; set; }
        public string CodAreaHsec { get; set; }
        public string CodPersona { get; set; }
        //public string Token { get; set; }
        public int Pagina { get; set; } 
        public int PaginaTamanio { get; set; }

        public class GetObservacionesBuscarQueryHandler : IRequestHandler<GetObservacionesBuscarQuery, ObservacionesVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IHttpContextAccessor _httpContext;


            public GetObservacionesBuscarQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
                _httpContext = HttpContext;
            }

            public async Task<ObservacionesVM> Handle(GetObservacionesBuscarQuery request, CancellationToken cancellationToken)
            {
                try{

                var vm = new ObservacionesVM();

                    // request.CodPersona = await _generalService.GetPersonaCodigo(request.CodPersona);

                    string tipo = null;
                    string subtipo = null;
                    if(request.CodTipoObservacion != null)
                    {
                        tipo = request.CodTipoObservacion.Substring(0, 1);
                        subtipo = (request.CodTipoObservacion.Length > 1) ? request.CodTipoObservacion.Substring(2,1) : null;
                    }

                    var userToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    var rolToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                    var filerListQuery = _context.TObservaciones
                    .Where(obs =>
                            (obs.Estado == true)
                        && (String.IsNullOrEmpty(request.NroObservacion) || obs.CodObservacion.Equals(request.NroObservacion))
                        && (String.IsNullOrEmpty(tipo) || obs.CodTipoObservacion.Equals(Int32.Parse(tipo)))
                        && (String.IsNullOrEmpty(subtipo) || obs.CodSubTipoObs.Equals(subtipo))
                        && (String.IsNullOrEmpty(request.CodNivelRiesgo) || obs.CodNivelRiesgo.Equals(request.CodNivelRiesgo))
                        && (String.IsNullOrEmpty(request.CodAreaHsec) || obs.CodAreaHsec.Equals(request.CodAreaHsec))
                        && (String.IsNullOrEmpty(request.CodGerencia) || obs.CodPosicionGer.Equals(request.CodGerencia))
                        && (String.IsNullOrEmpty(request.CodSuperIntendencia) || obs.CodPosicionSup.Equals(request.CodSuperIntendencia))
                        && (String.IsNullOrEmpty(request.CodPersona) || obs.CodObservadoPor.Contains(request.CodPersona))
                         && ( (obs.FechaObservacion < Convert.ToDateTime(request.FechaFin).AddDays(1)))
                        // && ( (obs.FechaObservacion = Convert.ToDateTime(request.FechaFin) )
                        && ( (obs.FechaObservacion > Convert.ToDateTime(request.FechaInicio) ) || (obs.FechaObservacion == Convert.ToDateTime(request.FechaInicio) ))
                        // &&  (obs.FechaObservacion == Convert.ToDateTime(request.FechaInicio) )
                        && (obs.CreadoPor.Contains(userToken) || rolToken!="6")
                        );

                vm.Count = filerListQuery.Count();

                // var ListQuery = new List<ObservacionDto>();
                var ListQuery = filerListQuery
                    .OrderByDescending(o => o.CodObservacion)
                    .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                    .Take(request.PaginaTamanio)
                    .ProjectTo<ObservacionDto>(_mapper.ConfigurationProvider)
                    .ToList();

                    //var maestro = await _generalService.GetMaestros();
                    var maestro = await _mediator.Send( new GetMaestroDataQuery() );
                foreach (var item in ListQuery)
                {

                    item.CodAreaHsec = buscarPorCodigo(maestro, "AREAHSEC", item.CodAreaHsec);
                    item.CodNivelRiesgo = buscarPorCodigo(maestro, "NIVELRIESGO", item.CodNivelRiesgo);
                    // item.CodTipoObservacion = item.CodTipoObservacion.Replace('_',' ');///juntar null or ''
                    item.CodTipoObservacion = (item.CodSubTipoObs != null && item.CodSubTipoObs != "") ? item.CodTipoObservacion + "." + item.CodSubTipoObs : item.CodTipoObservacion;
                    //item.CodPosicionGer = await _generalService.GetJeraquias(item.CodPosicionGer);
                    item.CodPosicionGer = await _mediator.Send(new Code2NameQuery() { codigo = item.CodPosicionGer });
                    //item.CodPosicionSup = await _generalService.GetJeraquias(item.CodPosicionSup);
                    item.CodPosicionSup = await _mediator.Send(new Code2NameQuery() { codigo = item.CodPosicionSup });
                    //item.CodSubUbicacion = await _generalService.GetUbicaciones(item.CodSubUbicacion);
                    item.CodSubUbicacion = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = item.CodSubUbicacion });
                    //item.CodUbicacion = await _generalService.GetUbicaciones(item.CodUbicacion);
                    item.CodUbicacion = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = item.CodUbicacion });
                    //item.CodUbicacionEspecifica = await _generalService.GetUbicaciones(item.CodUbicacionEspecifica);
                    item.CodUbicacionEspecifica = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = item.CodUbicacionEspecifica });
                    //item.CodObservadoPor = await _generalService.GetPersonas(item.CodObservadoPor);
                    item.CodObservadoPor = await _mediator.Send(new GetCode2NameOneQuery() { code = item.CodObservadoPor });
                    item.Editable = item.UsuCreacion == userToken || (new string[] { "1", "4" }).Contains(rolToken);
                    string COD_OBSERVACION = item.CodObservacion;
                    
                    if (item.CodTipoObservacion == "1") //if (item.CodTipoObservacion == TipoObservacion.Comportamiento.ToString())
                        {
                        var comportamiento = _context.TObservacionComportamientos.Find(COD_OBSERVACION);
                        if (comportamiento != null){
                            item.CodStopWork = buscarPorCodigo(maestro, "TOSW", comportamiento.CodStopWork);
                            item.CodActoSubEstandar = buscarPorCodigo(maestro, "ACTO_SUB_ESTANDAR", comportamiento.CodActoSubEstandar);
                        } 
                    }
                    else if (item.CodTipoObservacion == "2") // TipoObservacion.Condicion.ToString()
                        {
                        var condicion = _context.TObservacionCondiciones.Find(COD_OBSERVACION);
                        if (condicion != null) {
                            item.CodStopWork = buscarPorCodigo(maestro, "TOSW", condicion.CodStopWork);
                            item.CodCondSubEstandar = buscarPorCodigo(maestro, "CONDICION_SUB_ESTANDAR", condicion.CodCondicionSubEstandar);
                        }
                    }
                    else if (item.CodTipoObservacion == "3") //TipoObservacion.Tarea.ToString()
                        {
                        var tarea = _context.TObservacionTareas.Find(COD_OBSERVACION);
                        if (tarea != null){
                            item.CodStopWork = buscarPorCodigo(maestro, "TOSW", tarea.CodStopWork);
                        }
                    }
                    else if (item.CodTipoObservacion == "4.1") //TipoObservacion.Iteraccion_Seguridad.ToString()
                        {
                        var iteraccion = _context.TObservacionIteracciones.Find(COD_OBSERVACION);
                        if (iteraccion != null){
                            item.CodStopWork = buscarPorCodigo(maestro, "TOSW", iteraccion.CodStopWork);
                        }
                    }
                    
                }

                vm.Lists = ListQuery;

                return vm;
                }
                catch(Exception e){
                    Exception ee = e;
                    throw e;
                }
            }
        
            private string buscarPorCodigo(ICollection<MaestroDataVM> texto,string table,string codigo)
            {
                try
                {
                    if (texto == null) return codigo;
                    var list = texto.Where(t => t.CodTabla.Equals(table)).Select(t => t.Tipos).FirstOrDefault();
                    var a = list.Where(t => t.CodRegistro.Equals(codigo)).Select(t => t.Descripcion).First();
                    return a;
                }
                catch(Exception e)
                {
                    return codigo;
                }
                
            }
        }
    }
}
