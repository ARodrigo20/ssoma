using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.General.PlanAnualGeneral.Models;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.PlanAnualGeneral.Queries.GetModulosSeguimiento;
using Hsec.Application.General.PlanAnualGeneral.Queries.GetModulosSeguimientoTotal;

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetPlanAnualSeguimientoTotal
{
    public class GetPlanAnualSeguimientoTotalQuery : IRequest<GetPlanAnualSeguimientoTotalVM>
    {
        public FiltrosPlanAnualGeneralSeguimientoTotal filtros { get; set; }
        public class GetPlanAnualSeguimientoTotalHandler : IRequestHandler<GetPlanAnualSeguimientoTotalQuery, GetPlanAnualSeguimientoTotalVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetPlanAnualSeguimientoTotalHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<GetPlanAnualSeguimientoTotalVM> Handle(GetPlanAnualSeguimientoTotalQuery request, CancellationToken cancellationToken)
            {
                var vm = new GetPlanAnualSeguimientoTotalVM();
                
                var filtros = request.filtros;

                // filtros.Persona

                foreach(var cod in filtros.CodReferencia){
                    var planeados = _context.TPlanAnualVerConCri
                        .Where(t => 
                            t.CodReferencia.Equals(cod)
                            && t.Anio.Equals(filtros.Anio)
                            && t.CodMes.Equals(filtros.Mes)
                            && (String.IsNullOrEmpty(filtros.Persona) || t.CodPersona.Equals(filtros.Persona))
                            )
                        .Select(t => Int32.Parse(t.Valor))
                        .ToHashSet()
                        .Sum();
                    int ejecutados = 0;
                    if(String.IsNullOrEmpty(filtros.Persona)){
                        //ejecutados = await _ejecutados.getTotal(cod,filtros.Gerencia,filtros.Anio,filtros.Mes);
                        ejecutados = await _mediator.Send(new GetModuloSeguimientoTotalQuery() {Modulo = cod, Gerencia = filtros.Gerencia, Anio = filtros.Anio, CodMes = filtros.Mes });
                    }
                    else{
                        //ejecutados = await _ejecutados.getPer(cod,filtros.Persona,filtros.Anio,filtros.Mes);
                        ejecutados = await _mediator.Send(new GetModuloSeguimientoQuery() { Modulo = cod, CodPersona = filtros.Persona, Anio = filtros.Anio, CodMes = filtros.Mes });
                    }
                    var dato = new ModuloEjeDto(cod,ejecutados,planeados);
                    vm.ListModulo.Add(dato);
                }
                
                return vm;
            }

        }
    }
}