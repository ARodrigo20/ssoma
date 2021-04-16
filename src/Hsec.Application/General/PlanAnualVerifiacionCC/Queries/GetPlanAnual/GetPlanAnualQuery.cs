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

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetPlanAnual
{
    public class GetPlanAnualGeneralQuery : IRequest<GetPlanAnualGeneralVM>
    {
        public FiltrosPlanAnualGeneral filtros { get; set; }
        public class GetPlanAnualGeneralQueryHandler : IRequestHandler<GetPlanAnualGeneralQuery, GetPlanAnualGeneralVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPlanAnualGeneralQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetPlanAnualGeneralVM> Handle(GetPlanAnualGeneralQuery request, CancellationToken cancellationToken)
            {
                var vm = new GetPlanAnualGeneralVM();
                var filtros = request.filtros;
                var JerPosicion = await _context.TJerarquia.FindAsync(filtros.Gerencia);
                IList<string> JerarquiaPersonas = null;
                if(JerPosicion!=null){
                    JerarquiaPersonas = _context.TJerarquia.Join(_context.TJerarquiaPersona, jer => jer.CodPosicion, jper => jper.CodPosicion, (jer, jper) => new { jer = jer, jper = jper })
                    .Where(tuple => (tuple.jer.PathJerarquia.Substring(0, JerPosicion.PathJerarquia.Length) == JerPosicion.PathJerarquia && tuple.jper.CodTipoPersona == 1))
                    .Select(t => t.jper.CodPersona)
                    .ToList();   
                }
                vm.Pagina = request.filtros.Pagina;
                vm.Count = JerarquiaPersonas.Count();

                if(filtros.CodReferencia==null || filtros.CodReferencia.Count == 0){
                    vm.Codigos = _context.TPlanAnualVerConCri.Where(t =>
                                (String.IsNullOrEmpty(filtros.Anio) || t.Anio.Equals(filtros.Anio))
                                && (String.IsNullOrEmpty(filtros.Mes) || t.CodMes.Equals(filtros.Mes))
                                && (JerarquiaPersonas.Contains(t.CodPersona))
                            )
                            .GroupBy(t => t.CodReferencia)
                            .Select(t => t.Key).ToHashSet();
                    filtros.CodReferencia = vm.Codigos;
                }
                else{
                    vm.Codigos = filtros.CodReferencia;
                }

                if ((filtros.Pagina) > (vm.Count / filtros.PaginaTamanio) + 1) filtros.Pagina = 1;
                
                var ListQuery = JerarquiaPersonas
                        .Skip(filtros.Pagina * filtros.PaginaTamanio - filtros.PaginaTamanio)
                        .Take(filtros.PaginaTamanio)
                        .ToHashSet();

                foreach (var persona in ListQuery)
                {
                    var codigos = _context.TPlanAnualVerConCri
                        .Where(t =>
                            (filtros.CodReferencia.Contains(t.CodReferencia))
                            && (String.IsNullOrEmpty(filtros.Anio) || t.Anio.Equals(filtros.Anio))
                            && (String.IsNullOrEmpty(filtros.Mes) || t.CodMes.Equals(filtros.Mes))
                            && (t.CodPersona.Equals(persona))
                        )
                        .Select(t => new PlanReferenciaDto(t.CodReferencia,t.Valor)).ToList();
                    var nuevo = new PersonaDto(persona,codigos);
                    var personaFull = _context.TPersona.Find(persona);
                    nuevo.Nombres = personaFull.ApellidoPaterno + " " + personaFull.ApellidoMaterno + "," + personaFull.Nombres;
                    vm.list.Add(nuevo);
                }
                
                return vm;
            }
        }
    }
}