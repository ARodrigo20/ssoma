using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Incidentes.Queries.GetBuscarInsidentes
{

    public class GetBuscarAuditoriaQuery : IRequest<BuscarAuditoriaVM>
    {
        public string TipoAuditoria { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        //public string CodPersona { get; set; }

        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }
        public class GetBuscarAuditoriaQueryHandler : IRequestHandler<GetBuscarAuditoriaQuery, BuscarAuditoriaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetBuscarAuditoriaQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<BuscarAuditoriaVM> Handle(GetBuscarAuditoriaQuery request, CancellationToken cancellationToken)
            {
                var vm = new BuscarAuditoriaVM();

                var filerListQuery = _context.TAuditoria
                    .Where(aud =>
                            (aud.Estado == true)
                        && (String.IsNullOrEmpty(request.TipoAuditoria) || aud.CodTipoAuditoria.Equals(request.TipoAuditoria))
                        && (String.IsNullOrEmpty(request.FechaInicio) || aud.FechaInicio >= Convert.ToDateTime(request.FechaInicio))
                        && (String.IsNullOrEmpty(request.FechaFin) || aud.FechaFin <= Convert.ToDateTime(request.FechaFin))
                        );
                

                vm.Count = filerListQuery.Count();

                var ListQuery = filerListQuery
                    .OrderByDescending(i => i.CodAuditoria)
                    .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                    .Take(request.PaginaTamanio)
                    .ProjectTo<BuscarAuditoriaDto>(_mapper.ConfigurationProvider)
                    .ToHashSet();

                vm.List = ListQuery;


                // var maestro = await _generalService.GetMaestros();


                // foreach (var item in ListQuery)
                // {

                //     item.CodProveedor = await _generalService.GetProveedor(item.CodProveedor);
                //     item.CodRespGerencia = await _generalService.GetPersonas(item.CodRespGerencia);
                //     item.CodRespProveedor = await _generalService.GetPersonas(item.CodRespProveedor);
                //     item.CodPerReporta = await _generalService.GetPersonas(item.CodPerReporta);
                //     item.CodRespSuperint = await _generalService.GetPersonas(item.CodRespSuperint);
                //     item.CodPosicionGer = await _generalService.GetJeraquias(item.CodPosicionGer);
                //     item.CodPerReporta = await _generalService.GetJeraquias(item.CodPerReporta);
                //     item.CodPosicionSup = await _generalService.GetJeraquias(item.CodPosicionSup);
                //     item.CodSubUbicacion = await _generalService.GetUbicaciones(item.CodSubUbicacion);
                //     item.CodUbicacion = await _generalService.GetUbicaciones(item.CodUbicacion);
                //     item.CodTipoIncidente = await _generalService.GetTipoIncidente(item.CodTipoIncidente);
                //     item.CodSubTipoIncidente = await _generalService.GetTipoIncidente(item.CodSubTipoIncidente);
                //     item.CodUbicacionEspecifica = await _generalService.GetUbicaciones(item.CodUbicacionEspecifica);
                //     item.CodAreaHsec = buscarPorCodigo(maestro, "AREAHSEC", item.CodAreaHsec);
                //     item.CodActiRelacionada = buscarPorCodigo(maestro, "ACTIVIDADRELACIONADA", item.CodActiRelacionada);
                //     item.CodHha = buscarPorCodigo(maestro, "HHARELACIONADA", item.CodActiRelacionada);
                //     item.CodTituloInci = buscarPorCodigo(maestro, "TituloIncidente", item.CodTituloInci);
                //     item.CodTurno = buscarPorCodigo(maestro, "Turno", item.CodTurno);
                //     item.CodClasificaInci = buscarPorCodigo(maestro, "ClasificacionIncidente", item.CodClasificaInci);
                //     item.CodClasiPotencial = buscarPorCodigo(maestro, "ClasiPotencial", item.CodClasiPotencial);
                //     item.CodLesAper = buscarPorCodigo(maestro, "LesionesAPersonas", item.CodLesAper);

                // }

                // vm.Lists = ListQuery;

                return vm;
            }
   
        }
        
    }
}