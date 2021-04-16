using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.Incidentes;
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
using Hsec.Application.General.Ubicaciones.Queries.Code2Name;
using Hsec.Application.General.Personas.Queries.GetCode2NameOne;
using Hsec.Application.General.Jerarquias.Queries.Code2Name;
using Hsec.Application.General.Empresa.Queries.GetCode2Name;
using Hsec.Application.General.TipoIncidente.Queries.Code2Name;

namespace Hsec.Application.Incidentes.Queries.GetBuscarInsidentes
{

    public class GetBuscarInsidentesQuery : IRequest<IncidentesVM>
    {
        public string NroIncidente { get; set; }
        public string CodTituloIncidente { get; set; }
        public string CodUbicacion { get; set; }
        public string CodSubUbicacion { get; set; }
        public string CodContrata { get; set; }
        public string CodClasificaInci { get; set; }
        public string CodClasificacionPotencial { get; set; }
        public string CodTipoAfectado { get; set; }
        public string CodLesionPersonal { get; set; }
        public string CodGerencia { get; set; }
        public string CodSuperIntendencia { get; set; }
        public string CodAreaHSEC { get; set; }
        public string CodTipo { get; set; }
        public string CodSubtipo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        //public string CodPersona { get; set; }

        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }
        public class GetBuscarInsidentesQueryHandler : IRequestHandler<GetBuscarInsidentesQuery, IncidentesVM>
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

            public async Task<IncidentesVM> Handle(GetBuscarInsidentesQuery request, CancellationToken cancellationToken)
            {
                var vm = new IncidentesVM();

                var userToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var rolToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                var filerListQuery = _context.TIncidente
                    .Where(inc =>
                            (inc.Estado == true)
                        && (String.IsNullOrEmpty(request.NroIncidente) || inc.CodIncidente.Equals(request.NroIncidente))
                        && (String.IsNullOrEmpty(request.CodUbicacion) || inc.CodUbicacion.Equals(request.CodUbicacion))
                        && (String.IsNullOrEmpty(request.CodTituloIncidente) || inc.CodTituloInci.Equals(request.CodTituloIncidente))
                        && (String.IsNullOrEmpty(request.CodSubUbicacion) || inc.CodSubUbicacion.Equals(request.CodSubUbicacion))
                        && (String.IsNullOrEmpty(request.CodContrata) || inc.CodProveedor.Equals(request.CodContrata))
                        && (String.IsNullOrEmpty(request.CodClasificacionPotencial) || inc.CodClasiPotencial.Equals(request.CodClasificacionPotencial))
                        && (String.IsNullOrEmpty(request.CodClasificaInci) || inc.CodClasificaInci.Equals(request.CodClasificaInci))

                        && (String.IsNullOrEmpty(request.CodLesionPersonal) || inc.CodLesAper.Equals(request.CodLesionPersonal))
                        && (String.IsNullOrEmpty(request.CodGerencia) || inc.CodPosicionGer.Equals(request.CodGerencia))
                        && (String.IsNullOrEmpty(request.CodSuperIntendencia) || inc.CodPosicionSup.Equals(request.CodSuperIntendencia))
                        && (String.IsNullOrEmpty(request.CodAreaHSEC) || inc.CodAreaHsec.Equals(request.CodAreaHSEC))
                        && (String.IsNullOrEmpty(request.CodTipo) || inc.CodTipoIncidente.Equals(request.CodTipo))
                        && (String.IsNullOrEmpty(request.CodSubtipo) || inc.CodSubTipoIncidente.Equals(request.CodSubtipo))
                        && (String.IsNullOrEmpty(request.FechaInicio) || inc.FechaDelSuceso >= Convert.ToDateTime(request.FechaInicio).AddSeconds(-1))
                        && (String.IsNullOrEmpty(request.FechaFin) || inc.FechaDelSuceso <= Convert.ToDateTime(request.FechaFin).AddSeconds(1))
                        && (inc.CreadoPor.Contains(userToken) || rolToken != "6")
                        );
                
                TipoAfectado CodTipoAfectado;
                if(String.IsNullOrEmpty(request.CodTipoAfectado)) CodTipoAfectado = TipoAfectado.All;
                else CodTipoAfectado = (TipoAfectado) Enum.Parse(typeof(TipoAfectado), request.CodTipoAfectado, true);

                switch(CodTipoAfectado){
                    case TipoAfectado.Comunidad:
                        filerListQuery = filerListQuery.Include(t => t.TafectadoComunidad).Where(inc => inc.TafectadoComunidad.Count > 0);
                        break;
                    case TipoAfectado.Medio_Ambiente:
                        filerListQuery = filerListQuery.Include(t => t.TafectadoMedioAmbiente).Where(inc => inc.TafectadoMedioAmbiente.Count > 0);
                        break;
                    case TipoAfectado.Propiedad:
                        filerListQuery = filerListQuery.Include(t => t.TafectadoPropiedad).Where(inc => inc.TafectadoPropiedad.Count > 0);
                        break;
                    case TipoAfectado.Persona:
                        filerListQuery = filerListQuery.Include(t => t.TinvestigaAfectado).Where(inc => inc.TinvestigaAfectado.Count > 0);
                        break;
                    default:
                        break;
                }
                    // .Include(t => t.TafectadoComunidad)
                    // .Include(t => t.TafectadoMedioAmbiente)
                    // .Include(t => t.TafectadoPropiedad)
                    // .Include(t => t.TinvestigaAfectado)
                    // .Where(inc =>
                    //        (String.IsNullOrEmpty(request.CodTipoAfectado)
                    //           || (request.CodTipoAfectado.Equals(TipoAfectado.Persona) && inc.TinvestigaAfectado.Count > 0))
                    //     && (String.IsNullOrEmpty(request.CodTipoAfectado)
                    //           || (request.CodTipoAfectado.Equals(TipoAfectado.Comunidad) && inc.TafectadoComunidad.Count > 0))
                    //     && (String.IsNullOrEmpty(request.CodTipoAfectado)
                    //           || (request.CodTipoAfectado.Equals(TipoAfectado.Medio_Ambiente) && inc.TafectadoMedioAmbiente.Count > 0))
                    //     && (String.IsNullOrEmpty(request.CodTipoAfectado)
                    //           || (request.CodTipoAfectado.Equals(TipoAfectado.Propiedad) && inc.TafectadoPropiedad.Count > 0))
                    // );

                vm.Count = filerListQuery.Count();

                var ListQuery = filerListQuery
                    .OrderByDescending(i => i.CodIncidente)
                    .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                    .Take(request.PaginaTamanio)
                    .ProjectTo<IncidenteBuscarDto>(_mapper.ConfigurationProvider)
                    .ToHashSet();


                vm.Lists = ListQuery;


                //var maestro = await _generalService.GetMaestros();
                var maestro = await _mediator.Send(new GetMaestroDataQuery() { });


                foreach (var item in ListQuery)
                {

                    //item.CodProveedor = await _generalService.GetProveedor(item.CodProveedor);
                    item.CodProveedor = await _mediator.Send(new GetCode2NameQuery() { codigo = item.CodProveedor });
                    //item.CodRespGerencia = await _generalService.GetPersonas(item.CodRespGerencia);
                    item.CodRespGerencia = await _mediator.Send(new GetCode2NameOneQuery() { code = item.CodRespGerencia });
                    //item.CodRespProveedor = await _generalService.GetPersonas(item.CodRespProveedor);
                    item.CodRespProveedor = await _mediator.Send(new GetCode2NameOneQuery() { code = item.CodRespProveedor });
                    //item.CodPerReporta = await _generalService.GetPersonas(item.CodPerReporta);
                    item.CodPerReporta = await _mediator.Send(new GetCode2NameOneQuery() { code = item.CodPerReporta });
                    //item.CodRespSuperint = await _generalService.GetPersonas(item.CodRespSuperint);
                    item.CodRespSuperint = await _mediator.Send(new GetCode2NameOneQuery() { code = item.CodRespSuperint });
                    //item.CodPosicionGer = await _generalService.GetJeraquias(item.CodPosicionGer);
                    item.CodPosicionGer = await _mediator.Send(new Code2NameQuery() { codigo = item.CodPosicionGer });
                    //item.CodPerReporta = await _generalService.GetJeraquias(item.CodPerReporta);
                    //item.CodPosicionSup = await _generalService.GetJeraquias(item.CodPosicionSup);
                    item.CodPosicionSup = await _mediator.Send(new Code2NameQuery() { codigo = item.CodPosicionSup });
                    //item.CodSubUbicacion = await _generalService.GetUbicaciones(item.CodSubUbicacion);
                    item.CodSubUbicacion = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = item.CodSubUbicacion });
                    //item.CodUbicacion = await _generalService.GetUbicaciones(item.CodUbicacion);
                    item.CodUbicacion = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = item.CodUbicacion });
                    //item.CodUbicacionEspecifica = await _generalService.GetUbicaciones(item.CodUbicacionEspecifica);
                    item.CodUbicacionEspecifica = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = item.CodUbicacionEspecifica });

                    //item.CodTipoIncidente = await _generalService.GetTipoIncidente(item.CodTipoIncidente);
                    item.CodTipoIncidente = await _mediator.Send(new Code2NameTipoIncidenteQuery() { codigo = item.CodTipoIncidente });
                    //item.CodSubTipoIncidente = await _generalService.GetTipoIncidente(item.CodSubTipoIncidente);
                    item.CodSubTipoIncidente = await _mediator.Send(new Code2NameTipoIncidenteQuery() { codigo = item.CodTipoIncidente });

                    item.CodAreaHsec = buscarPorCodigo(maestro, "AREAHSEC", item.CodAreaHsec);
                    item.CodActiRelacionada = buscarPorCodigo(maestro, "ACTIVIDADRELACIONADA", item.CodActiRelacionada);
                    item.CodHha = buscarPorCodigo(maestro, "HHARELACIONADA", item.CodActiRelacionada);
                    item.CodTituloInci = buscarPorCodigo(maestro, "TituloIncidente", item.CodTituloInci);
                    item.CodTurno = buscarPorCodigo(maestro, "Turno", item.CodTurno);
                    item.CodClasificaInci = buscarPorCodigo(maestro, "ClasificacionIncidente", item.CodClasificaInci);
                    item.CodClasiPotencial = buscarPorCodigo(maestro, "ClasiPotencial", item.CodClasiPotencial);
                    item.CodLesAper = buscarPorCodigo(maestro, "LesionesAPersonas", item.CodLesAper);
                    item.Editable = item.UsuCreacion == userToken || (new string[] { "1", "4" }).Contains(rolToken);
                }

                vm.Lists = ListQuery;

                return vm;
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