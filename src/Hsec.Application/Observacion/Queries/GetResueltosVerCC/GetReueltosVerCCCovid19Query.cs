using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.PlanAnualCartillas.Queries.GetCartillasPorPerson;
using Hsec.Application.General.Cartilla.Queries.GetCartilla;
using Hsec.Application.General.Maestro.Queries.GetCode2Name;

namespace Hsec.Application.Observacion.Queries.GetResueltosVerCC
{
    public class GetReueltosVerCCCovid19Query : IRequest<ResueltosVM>
    {
        public string CodPersona { get; set; }
        public string Anio { get; set; }
        public string CodMes { get; set; }

        public class GetReueltosVerCCCovid19QueryHandler : IRequestHandler<GetReueltosVerCCCovid19Query, ResueltosVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly string COVID19_ID = "CCPF0000022";

            public GetReueltosVerCCCovid19QueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<ResueltosVM> Handle(GetReueltosVerCCCovid19Query request, CancellationToken cancellationToken)
            {
                var vm = new ResueltosVM();

                //var planificados = await _general.GetPlanificados(request.CodPersona, request.Anio, request.CodMes);
                var planificados = await _mediator.Send(new GetCartillasPorPersonasQuery() { CodPersona = request.CodPersona, Anio = request.Anio, CodMes = request.CodMes });

                var list_ejecutados = _context.TObservaciones.Where(t => 
                    t.CodObservadoPor.Equals(request.CodPersona)
                    && t.CodTipoObservacion == 4 && t.CodSubTipoObs == "2" /*TipoObservacion.VerificacionControlCritico*/
                    && t.FechaObservacion.Value.Year.Equals(Int32.Parse(request.Anio))
                    && t.FechaObservacion.Value.Month.Equals(Int32.Parse(request.CodMes)))
                    .Select(t => t.CodObservacion)
                    .ToList();
                
                foreach(var item in planificados.list){
                    CartillaDto temp = new CartillaDto();
                    temp.CodCartilla = item.CodReferencia;
                    temp.Pentidentes = Int32.Parse(item.Valor) - getNumeroPlanificados(request.CodMes,request.Anio,request.CodPersona,item.CodReferencia);
                    //var cartilla = await _general.GetCartilla(item.CodReferencia);
                    var cartilla = await _mediator.Send(new GetCartillaQuery() { CodCartilla = item.CodReferencia });
                    if(cartilla!=null && cartilla.PeligroFatal.Equals(COVID19_ID) && temp.Pentidentes>0){
                        temp.TipoCartilla = cartilla.TipoCartilla;
                        //temp.DescTipoCartilla = await _general.GetMaestroTabla("CCTV",cartilla.TipoCartilla); 
                        temp.DescTipoCartilla = await _mediator.Send(new GetCode2NameQuery() { CodTable = "CCTV", CodMaestro = cartilla.TipoCartilla });
                        temp.PeligroFatal = cartilla.PeligroFatal;
                        //temp.DescPeligroFatal = await _general.GetMaestroTabla("ControlCriticoPF",cartilla.PeligroFatal);
                        temp.DescPeligroFatal = await _mediator.Send(new GetCode2NameQuery() { CodTable = "ControlCriticoPF", CodMaestro = cartilla.PeligroFatal });

                        vm.list.Add(temp);
                    }
                }
                return vm;
            }

            private int getNumeroPlanificados(string CodMes,string CodAnio,string CodPersona,string CodCartilla){
                var list = _context.TObservaciones.Join( _context.TObservacionVerControlCritico
                    ,Obs => Obs.CodObservacion
                    ,ObsVCC => ObsVCC.CodObservacion
                    ,(x,y) => new { Obs = x, ObsVCC = y } )
                    .Where(tuple => 
                        tuple.Obs.CodTipoObservacion.Equals(4) && tuple.Obs.CodSubTipoObs.Equals("2") /*TipoObservacion.VerificacionControlCritico*/
                        && tuple.Obs.FechaObservacion.Value.Month == Int32.Parse(CodMes)
                        && tuple.Obs.FechaObservacion.Value.Year == Int32.Parse(CodAnio)
                        && tuple.Obs.CodObservadoPor.Equals(CodPersona)
                        && tuple.Obs.Estado == true
                        && tuple.ObsVCC.Estado == true
                        && tuple.ObsVCC.CodCartilla.Equals(CodCartilla)
                        )
                    .ToList();
                
                if(list == null) return 0;
                else return list.Count;
            }

        }
    }
}