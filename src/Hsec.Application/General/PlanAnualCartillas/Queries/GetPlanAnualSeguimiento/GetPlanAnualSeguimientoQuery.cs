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
using Hsec.Application.VerificacionObservacionCc.Queries.GetEjecutadosQuery;

namespace Hsec.Application.General.PlanAnualCartillas.Queries.GetPlanAnualSeguimiento
{
    public class GetPlanAnualSeguimientoQuery : IRequest<GetPlanAnualSeguimientoVM>
    {
        public FiltrosPlanAnualSeguimiento filtros { get; set; }
        public class GetPlanAnualSeguimientoQueryHandler : IRequestHandler<GetPlanAnualSeguimientoQuery, GetPlanAnualSeguimientoVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetPlanAnualSeguimientoQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<GetPlanAnualSeguimientoVM> Handle(GetPlanAnualSeguimientoQuery request, CancellationToken cancellationToken)
            {
                var vm = new GetPlanAnualSeguimientoVM();

                var filtros = request.filtros;

                var JerPosicion = await _context.TJerarquia.FindAsync(filtros.Gerencia);
                IList<string> JerarquiaPersonas = null;
                if (!string.IsNullOrEmpty(filtros.Persona))
                {
                    JerarquiaPersonas = new List<string>();
                    JerarquiaPersonas.Add(filtros.Persona);
                }
                else if (JerPosicion != null)
                {
                    JerarquiaPersonas = _context.TJerarquia.Join(_context.TJerarquiaPersona, jer => jer.CodPosicion, jper => jper.CodPosicion, (jer, jper) => new { jer = jer, jper = jper })
                    .Where(tuple => (tuple.jer.PathJerarquia.Substring(0, JerPosicion.PathJerarquia.Length) == JerPosicion.PathJerarquia && tuple.jper.CodTipoPersona == 1))
                    .Select(t => t.jper.CodPersona)
                    .ToList();
                }

                vm.Pagina = request.filtros.Pagina;
                vm.Count = JerarquiaPersonas.Count();

                if (filtros.CodReferencia == null || filtros.CodReferencia.Count == 0)
                {
                    vm.Codigos = _context.TPlanAnual.Where(t =>
                            (String.IsNullOrEmpty(filtros.Anio) || t.Anio.Equals(filtros.Anio))
                            && (String.IsNullOrEmpty(filtros.Mes) || t.CodMes.Equals(filtros.Mes))
                            && (JerarquiaPersonas.Contains(t.CodPersona))
                        )
                        .GroupBy(t => t.CodReferencia)
                        .Select(t => t.Key).ToHashSet();
                    filtros.CodReferencia = vm.Codigos;
                }
                else
                {
                    vm.Codigos = filtros.CodReferencia;
                }

                if ((filtros.Pagina) > (vm.Count / filtros.PaginaTamanio) + 1) filtros.Pagina = 1;

                var ListQuery = JerarquiaPersonas
                        .Skip(filtros.Pagina * filtros.PaginaTamanio - filtros.PaginaTamanio)
                        .Take(filtros.PaginaTamanio)
                        .ToHashSet();

                foreach (var persona in ListQuery)
                {
                    var nuevo = new PersonaPSADto();
                    nuevo.CodPersona = persona;
                    nuevo.Nombres = getPersonaFull(persona);
                    nuevo.ListCartilla = await getListPerson(filtros, persona);
                    vm.ListPersona.Add(nuevo);
                }

                return vm;
            }

            private string getPersonaFull(string persona)
            {
                var personaFull = _context.TPersona.Find(persona);
                if (personaFull == null) return "";
                return string.Format("{0} {1},{2}", personaFull.ApellidoPaterno, personaFull.ApellidoMaterno, personaFull.Nombres);
            }

            private async Task<ICollection<CartillaEjeDto>> getListPerson(FiltrosPlanAnualSeguimiento filtros, string persona)
            {

                //var EjecutadosXPersona = await _ejecutados.getCartillasEjecutadas(persona, filtros.Anio, filtros.Mes);
                var EjecutadosXPersona = await _mediator.Send(new GetEjecutadosQuery() { CodPersona = persona, Anio = filtros.Anio, CodMes = filtros.Mes } );


                var list = new HashSet<CartillaEjeDto>();
                foreach (var codigo in filtros.CodReferencia)
                {
                    var planAnual = _context.TPlanAnual
                        .Where(t =>
                            (codigo.Equals(t.CodReferencia))
                            && (String.IsNullOrEmpty(filtros.Anio) || t.Anio.Equals(filtros.Anio))
                            && (String.IsNullOrEmpty(filtros.Mes) || t.CodMes.Equals(filtros.Mes))
                            && (t.CodPersona.Equals(persona))
                        ).ToHashSet();
                    var planAnual2 = planAnual.Select(t => Int32.Parse(t.Valor));
                    var planAnual3 = planAnual2.Sum();

                    //var Ejecutados = await _ejecutados.getCartillasEjecutadas(persona, codigo, filtros.Anio, filtros.Mes);
                    //int Ejecutados = 1;
                    //var mod = new CartillaEjeDto(codigo, Ejecutados, planAnual3);
                    var Ejecutados = EjecutadosXPersona.List.ToList().Find(x => x.CodCartilla.Equals(codigo));


                    var mod = new CartillaEjeDto(codigo, (Ejecutados != null ? Ejecutados.Valor : 0), planAnual3);

                    if ((mod.Ejecutados != 0) || mod.Planeados != 0)
                    {
                        list.Add(mod);
                    }
                }
                return list;
            }
        }
    }
}