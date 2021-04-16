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

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetPlanAnualGeneral
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
                    vm.Codigos = _context.TPlanAnualGeneral.Where(t =>
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

                var Listperson = _context.TPersona.Where(p => ListQuery.Contains(p.CodPersona)).Select(t => new {t.CodPersona, Nombres=t.ApellidoPaterno + " " + t.ApellidoMaterno + "," + t.Nombres }).ToList();
                var PlanAnualPersons = _context.TPlanAnualGeneral
                        .Where(t =>
                            (filtros.CodReferencia.Contains(t.CodReferencia))
                            && (String.IsNullOrEmpty(filtros.Anio) || t.Anio.Equals(filtros.Anio))
                            && (String.IsNullOrEmpty(filtros.Mes) || t.CodMes.Equals(filtros.Mes))
                            && (ListQuery.Contains(t.CodPersona))
                        ).ToList().GroupBy(x => x.CodPersona)
                        .Select(g => new PersonaDto {
                            Nombres = Listperson.First(p => p.CodPersona == g.Key).Nombres,
                            CodPersona = g.Key,
                            ListCodigos = g.Select(lc => new PlanReferenciaDto { CodReferencia = lc.CodReferencia, Valor = lc.Valor }).ToList(),
                        }).ToList();//.OrderBy(r => r.Nombres);

                // agregar valores nulos a todos los elementos 
                var PlanAnualPersonsMoreCeros = new List<PersonaDto>();
                var modulosDefault = new List<PlanReferenciaDto>();
                foreach(var item in filtros.CodReferencia)
                {
                    var nuevo = new PlanReferenciaDto(item, "0");
                    modulosDefault.Add(nuevo);
                }

                foreach (var item in Listperson)
                {
                    var person = PlanAnualPersons.Find(t => t.CodPersona.Equals(item.CodPersona));
                    if (person!=null)
                    {
                        PlanAnualPersonsMoreCeros.Add(person);
                    }
                    else
                    {
                        var nuevo = new PersonaDto {
                                Nombres = item.Nombres,
                                CodPersona = item.CodPersona,
                                ListCodigos = modulosDefault
                        };
                        PlanAnualPersonsMoreCeros.Add(nuevo);
                    }
                }

                vm.list = PlanAnualPersonsMoreCeros;
                return vm;
                //List<Continent> List = MyRepository.GetList<GetAllCountriesAndCities>("EXEC sp_GetAllCountriesAndCities")
                //.GroupBy(x => x.ContinentName)
                //.Select(g => new Continent
                //{
                //    ContinentName = g.Key,
                //    Countries = g.GroupBy(x => x.CountryName)
                //                 .Select(cg => new Country
                //                 {
                //                     CountryName = cg.Key,
                //                     Cities = cg.GroupBy(x => x.CityName)
                //                                .Select(cityG => new City { CityName = cityG.Key })
                //                                .ToList()
                //                 })
                //                 .ToList()
                //})
                //.ToList();

                //foreach (var persona in Listperson)
                //{
                //    var codigos = PlanAnualPersons.Where(t =>t.CodPersona.Equals(persona.CodPersona)).Select(t => new PlanReferenciaDto(t.CodReferencia,t.Valor)).ToHashSet();
                //    var nuevo = new PersonaDto(persona.CodPersona,codigos);
                //    nuevo.Nombres = persona.Nombres;
                //    vm.list.Add(nuevo);
                //}                

            }
        }
    }
}