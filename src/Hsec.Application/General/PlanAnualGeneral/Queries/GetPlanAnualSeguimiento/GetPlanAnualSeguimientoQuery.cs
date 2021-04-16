using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.General.PlanAnualGeneral.Models;
using Hsec.Domain.Entities.General;
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

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetPlanAnualSeguimiento
{
    public class GetPlanAnualSeguimientoQuery : IRequest<GetPlanAnualSeguimientoVM>
    {
        public FiltrosPlanAnualGeneralSeguimiento filtros { get; set; }
        public class GetPlanAnualSeguimientoQueryHandler : IRequestHandler<GetPlanAnualSeguimientoQuery, GetPlanAnualSeguimientoVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetPlanAnualSeguimientoQueryHandler(IApplicationDbContext context, IMapper mapper,IMediator mediator)
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
                if(!string.IsNullOrEmpty(filtros.Persona)){
                    JerarquiaPersonas = new List<string>();
                    JerarquiaPersonas.Add(filtros.Persona);
                }
                else if(JerPosicion!=null){
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
                        .ToList();

                var Listperson = _context.TPersona.Where(p => ListQuery.Contains(p.CodPersona)).Select(t => new { t.CodPersona, Nombres = t.ApellidoPaterno + " " + t.ApellidoMaterno + "," + t.Nombres }).ToList();
                var PlanAnualPersons = _context.TPlanAnualGeneral
                        .Where(t =>
                            (filtros.CodReferencia.Contains(t.CodReferencia))
                            && (String.IsNullOrEmpty(filtros.Anio) || t.Anio.Equals(filtros.Anio))
                            && (String.IsNullOrEmpty(filtros.Mes) || t.CodMes.Equals(filtros.Mes))
                            && (ListQuery.Contains(t.CodPersona))
                        ).ToList().GroupBy(x => x.CodPersona)
                        .Select(g => new PersonaPSADto
                        {
                            Nombres = Listperson.First(p => p.CodPersona == g.Key).Nombres,
                            CodPersona = g.Key,
                            ListModulo = g.Select(lc => new ModuloEjeDto(lc.CodReferencia, 0, Int32.Parse(lc.Valor))).ToList(),
                        }).ToList();

                foreach (var codM in vm.Codigos)
                {
                    //var Ejecutados = await _ejecutados.getLisPer(codM, Int32.Parse(filtros.Anio), Int32.Parse(filtros.Mes), ListQuery);
                    //var Ejecutados = await _ejecutados.getLisPer(codM, Int32.Parse(filtros.Anio), Int32.Parse(filtros.Mes), ListQuery);
                    var Ejecutados = await getLisPer(codM, Int32.Parse(filtros.Anio), Int32.Parse(filtros.Mes), ListQuery);
                    foreach (var tuplePer in Ejecutados)
                    {
                        var tempPer = PlanAnualPersons.Where(p => p.CodPersona == tuplePer.Item1).FirstOrDefault();
                        if (tempPer == null)
                        {
                            var NewPerson = new PersonaPSADto()
                            {
                                Nombres = Listperson.First(p => p.CodPersona == tuplePer.Item1).Nombres,
                                CodPersona = tuplePer.Item1
                            };
                            NewPerson.ListModulo.Add(new ModuloEjeDto(codM, tuplePer.Item2, 0));
                            PlanAnualPersons.Add(NewPerson);
                        }
                        else
                        {
                            var tempMod = tempPer.ListModulo.Where(m => m.Codigo == codM).FirstOrDefault();
                            if (tempMod == null) tempPer.ListModulo.Add(new ModuloEjeDto(codM, tuplePer.Item2, 0));
                            else tempMod.Ejecutados = tuplePer.Item2;
                        }
                    }
                }

                vm.ListPersona = PlanAnualPersons;
                vm.Count = PlanAnualPersons.Count();

                //foreach (var persona in ListQuery)
                //{
                //    var nuevo = new PersonaPSADto();
                //    nuevo.CodPersona = persona;
                //    nuevo.Nombres = getPersonaFull(persona);
                //    nuevo.ListModulo = await getListPerson(filtros, persona);
                //    vm.ListPersona.Add(nuevo);
                //}

                return vm;
            }

            private string getPersonaFull(string persona){
                var personaFull = _context.TPersona.Find(persona);
                if(personaFull == null) return "";
                return personaFull.ApellidoPaterno + " " + personaFull.ApellidoMaterno + "," + personaFull.Nombres; //string.Format("{0} {1},{2}",personaFull.ApellidoPaterno,personaFull.ApellidoMaterno,personaFull.Nombres);
            }

            //private async Task<ICollection<ModuloEjeDto>> getListPerson (FiltrosPlanAnualGeneralSeguimiento filtros,string persona){

            //    var list = new HashSet<ModuloEjeDto>();
            //    foreach (var codigo in filtros.CodReferencia)
            //    {
            //        var planAnual = _context.TPlanAnualGeneral
            //            .Where(t =>
            //                (codigo.Equals(t.CodReferencia))
            //                && (String.IsNullOrEmpty(filtros.Anio) || t.Anio.Equals(filtros.Anio))
            //                && (String.IsNullOrEmpty(filtros.Mes) || t.CodMes.Equals(filtros.Mes))
            //                && (t.CodPersona.Equals(persona))
            //            ).ToHashSet();
            //        var planAnual2 = planAnual.Select(t => Int32.Parse(t.Valor));
            //        var planAnual3 = planAnual2.Sum();

            //        //var Ejecutados = await _ejecutados.getPer(codigo,persona,filtros.Anio,filtros.Mes);
            //        //var Ejecutados = await _mediator.Send(new GetModuloSeguimientoQuery() { Modulo = codigo, CodPersona = persona, Anio = filtros.Anio, CodMes = filtros.Mes });
            //        var Ejecutados = await getEjecutados(codigo, persona,filtros.Anio, filtros.Mes);

            //        var mod = new ModuloEjeDto(codigo, Ejecutados, planAnual3);

            //        if (Ejecutados != 0 || planAnual3 != 0)
            //        {
            //            list.Add(mod);
            //        }
            //    }
            //    return list;
            //}

            //Modulos
            //public async Task<int> getEjecutados(string modulo, string persona, string anio, string mes)
            //{
            //    var resp = 0;

            //    if (modulo.Equals("01.04")) // acto condicion - comportamiento y condicion
            //    {
            //        resp = _context.TObservaciones.Where(
            //            t => t.Estado == true
            //            && (t.CreadoPor.Equals(persona) || t.CodObservadoPor.Equals(persona))
            //            && t.FechaObservacion.Value.Year.Equals(anio)
            //            && t.FechaObservacion.Value.Month.Equals(mes)
            //            && (t.CodTipoObservacion == 1 || t.CodTipoObservacion == 2)
            //            )
            //            .Count();
            //    }
            //    if (modulo.Equals("01.01")) // Tarea
            //    {
            //        resp = _context.TObservaciones.Where(
            //            t => t.Estado == true
            //            && (t.CreadoPor.Equals(persona) || t.CodObservadoPor.Equals(persona))
            //            && t.FechaObservacion.Value.Year.Equals(anio)
            //            && t.FechaObservacion.Value.Month.Equals(mes)
            //            && (t.CodTipoObservacion == 3)
            //            )
            //            .Count();
            //    }
            //    if (modulo.Equals("01.05")) // interaccion seguridad
            //    {
            //        resp = _context.TObservaciones.Where(
            //            t => t.Estado == true
            //            && (t.CreadoPor.Equals(persona) || t.CodObservadoPor.Equals(persona))
            //            && t.FechaObservacion.Value.Year.Equals(anio)
            //            && t.FechaObservacion.Value.Month.Equals(mes)
            //            && (t.CodTipoObservacion == 4)
            //            )
            //            .Count();
            //    }

            //    return resp;
            //}

            public async Task<List<Tuple<string, int>>> getLisPer(string modulo, int anio, int mes, List<string> personas)
            {
                if (modulo.Equals("01.04") || modulo.Equals("01.01") || modulo.Equals("01.05"))
                {
                    List<int> opt = new List<int>();
                    switch (modulo)
                    {
                        case "01.04":
                            opt.Add(1);
                            opt.Add(2);
                            break;
                        case "01.01":
                            opt.Add(3);
                            break;
                        case "01.05":
                            opt.Add(4);
                            break;
                    }
                    return _context.TObservaciones.Where(
                            t => t.Estado == true
                            && personas.Contains(t.CodObservadoPor)
                            && t.FechaObservacion.Value.Year.Equals(anio)
                            && t.FechaObservacion.Value.Month.Equals(mes)
                            && (opt.Contains(t.CodTipoObservacion) && (t.CodSubTipoObs == "1" || t.CodSubTipoObs == "2" || t.CodSubTipoObs == "3" || t.CodSubTipoObs == null || !opt.Contains(4)))
                            ).GroupBy(t => t.CodObservadoPor).Select(g => new Tuple<string, int>(g.Key, g.Count())).ToList();
                }
                ////Inspecciones
                else if (modulo.Equals("02.01") || modulo.Equals("02.02") || modulo.Equals("02.03") || modulo.Equals("02.04"))
                {
                    var codTipo = "0";
                    if (modulo.Equals("02.01")) codTipo = "1";
                    else if (modulo.Equals("02.02")) codTipo = "2";
                    else if (modulo.Equals("02.03")) codTipo = "3";
                    else if (modulo.Equals("02.04")) codTipo = "4";

                    return _context.TEquipoInspeccion.Join(_context.TInspeccion, jer => jer.CodInspeccion, jper => jper.CodInspeccion, (jer, jper) => new { jer = jer, jper = jper })
                     .Where(tuple => (tuple.jer.Estado && tuple.jper.Estado && tuple.jper.CodTipo == codTipo && tuple.jper.Fecha.Value.Year == anio && tuple.jper.Fecha.Value.Month == mes && personas.Contains(tuple.jer.CodPersona)))
                     .GroupBy(t => t.jer.CodPersona).Select(g => new Tuple<string, int>(g.Key, g.Count())).ToList();
                }
                ////Incidentes
                else if (modulo.Equals("03.01"))
                {

                    return _context.TIncidente
                    .Where(
                        t => t.Estado == true
                        && personas.Contains(t.CodPerReporta)
                        && t.Creado.Year.Equals(anio)
                        && t.Creado.Month.Equals(mes)
                        )
                    .GroupBy(t => t.CodPerReporta)
                    .Select(g => new Tuple<string, int>(g.Key, g.Count()))
                    .ToList();
                }
                ////Auditorias
                else if (modulo.Equals("05.01"))
                {

                    return _context.TAuditoria
                    .Where(
                        t => t.Estado == true
                        && personas.Contains(t.CodRespAuditoria)
                        && t.Creado.Year.Equals(anio)
                        && t.Creado.Month.Equals(mes)
                        )
                    .GroupBy(t => t.CodRespAuditoria)
                    .Select(g => new Tuple<string, int>(g.Key, g.Count()))
                    .ToList();
                }
                ////Reuniones
                else if (modulo.Equals("07.01"))
                {

                    return _context.TReunion
                    .Where(
                        t => t.Estado == true
                        && personas.Contains(t.CodPerFacilitador)
                        && t.Creado.Year.Equals(anio)
                        && t.Creado.Month.Equals(mes)
                        )
                    .GroupBy(t => t.CodPerFacilitador)
                    .Select(g => new Tuple<string, int>(g.Key, g.Count()))
                    .ToList();
                }
                ///Comites
                else if (modulo.Equals("08.01"))
                {
                    return _context.TListaParticipantesComite.Join(_context.TComite, jer => jer.CodComite, jper => jper.CodComite, (jer, jper) => new { jer = jer, jper = jper })
                    .Where(tuple => (tuple.jer.Estado && tuple.jper.Estado && tuple.jper.Fecha.Year == anio && tuple.jper.Fecha.Month == mes && personas.Contains(tuple.jer.CodPersona)))
                    .GroupBy(t => t.jer.CodPersona).Select(g => new Tuple<string, int>(g.Key, g.Count())).ToList();
                }
                ///YoAseguro
                else if (modulo.Equals("10.01"))
                {
                    return _context.TYoAseguro
                    .Where(
                        t => t.Estado == true
                        && personas.Contains(t.CodPersonaResponsable)
                        && t.Creado.Year.Equals(anio)
                        && t.Creado.Month.Equals(mes)
                        )
                    .GroupBy(t => t.CodPersonaResponsable)
                    .Select(g => new Tuple<string, int>(g.Key, g.Count()))
                    .ToList();
                }
                ///Verificaciones
                else if (modulo.Equals("12.01") || modulo.Equals("12.02"))
                {
                    string mod;
                    if (modulo.Equals("12.01")) mod = TipoVerificacion.IPERC_Continuo;
                    else if (modulo.Equals("12.02")) mod = TipoVerificacion.PTAR;
                    else return null;

                    return _context.TVerificaciones.Where(
                        t => t.Estado
                        && personas.Contains(t.CodVerificacionPor)
                        && t.CodTipoVerificacion.Equals(mod)
                        && t.FechaVerificacion.Year.Equals(anio)
                        && t.FechaVerificacion.Month.Equals(mes)
                        ).GroupBy(t => t.CodVerificacionPor).Select(g => new Tuple<string, int>(g.Key, g.Count())).ToList();
                }
                else
                {
                    return new List<Tuple<string, int>>();
                }
            }
        }
    }
}
