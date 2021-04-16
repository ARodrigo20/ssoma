using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Preguntas_Mis_Capacitaciones.Queries.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Hsec.Application.Capacitaciones.Preguntas_Mis_Capacitaciones.Queries.DTOs;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Inscritos.Queries.DTOs;
using Hsec.Application.General.Maestro.Queries.GetTablaQuery;
using Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiasPersona;

namespace Hsec.Application.Capacitaciones.Preguntas_Mis_Capacitaciones.Queries
{
    public class GetMisCapacitacionesQuery : IRequest<GetMisCapacitacionesVM>
    {
        public string codPersona { get; set; }
        public class GetMisCapacitacionesQueryHandler : IRequestHandler<GetMisCapacitacionesQuery, GetMisCapacitacionesVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public GetMisCapacitacionesQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<GetMisCapacitacionesVM> Handle(GetMisCapacitacionesQuery request, CancellationToken cancellationToken)
            {
                GetMisCapacitacionesVM GeneralVM = new GetMisCapacitacionesVM();
                string codPersona = request.codPersona;
                //TRAER AL MAS ACTUAL!!
                //&& MayorDateTime(cursos.FechaFin, cursos.FechaInicio)
                var cursosdasboard = (from part in _context.TParticipantes.Where(i => i.Estado)
                                      join cursos in _context.TCurso.Where(i => i.Estado) on part.CodCurso equals cursos.CodCurso
                                      join tema in _context.TTemaCapacitacion.Where(i => i.Estado) on cursos.CodTemaCapacita equals tema.CodTemaCapacita
                                      into lista
                                      from list in lista.DefaultIfEmpty()
                                      where (part.Estado && cursos.Estado && list.Estado && part.CodPersona == codPersona && part.Evaluado && part.Nota != null && part.Nota != 0) 
                                      select new GetMisCapacitacionesDTO
                                      {
                                          codTemaCapacita = cursos.CodTemaCapacita,
                                          codCurso = cursos.CodCurso,
                                          tema = list.Descripcion,
                                          fechaFinal = cursos.FechaFin,
                                          codVigenciaCapacita = cursos.CodVigenciaCapacita,
                                          vigencia = cursos.Vigencia,
                                          fechaIni = cursos.FechaInicio,
                                          nota = part.Nota,
                                          fechaVen= part.Modificado,
                                          intentos= part.Intentos,
                                          notabase= cursos.PuntajeTotal,
                                          porcentaje= cursos.PorcAprobacion,
                                          certificado=false
                                      }).ToList();

                var distinctItems = cursosdasboard.OrderByDescending(i => i.fechaIni).GroupBy(x => x.codTemaCapacita).Select(y => y.First()).ToList();
                //List<MaestroDTO> EstadosCurso = await _maestroService.GetMaestroTabla("EstadoCurso");
                TablaVM EstadosCurso = await _mediator.Send(new GetTablaQuery() { CodTabla = "EstadoCurso" });

                if (distinctItems.Count > 0)
                {
                    //utilizar el servicio adicional!!
                    //var val = await _jerPos.JerarquiaPersonaPosicion(codPersona);
                    var val = await _mediator.Send(new JerarquiaPersonaGSQuery() { codPersona = codPersona });

                    var codPosicion = val.count>0?val.data[0].data + "":"x_x";
                    var temasPerfil = _context.TPlanTema.Where(p => p.CodReferencia == codPosicion || p.CodReferencia == codPersona).ToList();

                    foreach (var it in distinctItems)
                    {

                        DateTime fechFin = new DateTime();
                        if (it.codVigenciaCapacita != "5")
                        {

                            //var dateTime = new DateTime(it.fechaIni.Year, it.fechaIni.Month, 1);
                            var dateTime = it.fechaVen??it.fechaIni;                            

                           DateTime dateADD = new DateTime();
                            if ((it.codVigenciaCapacita == "1"))
                            {
                                dateADD = dateTime.AddDays(int.Parse(it.vigencia.ToString()));
                            }
                            else if (it.codVigenciaCapacita == "2")
                            {
                                dateADD = dateTime.AddDays((7) * (int.Parse(it.vigencia.ToString())));
                            }
                            else if (it.codVigenciaCapacita == "3")
                            {
                                dateADD = dateTime.AddMonths(int.Parse(it.vigencia.ToString()));
                            }
                            else if (it.codVigenciaCapacita == "4")
                            {
                                dateADD = dateTime.AddYears(int.Parse(it.vigencia.ToString()));
                            }
                            fechFin = dateADD;
                        }
                        else
                        {
                            fechFin = it.fechaFinal;
                        }                       
                        it.fechaVen = fechFin;
                        if (fechFin < new DateTime())
                        {
                            it.estado = "4";
                            if (it.notabase == 0) it.notabase = new decimal(0.001);
                            if ((it.nota * 100 / it.notabase) >= it.porcentaje) it.certificado = true;
                        }
                        else if (temasPerfil.Any(t => t.CodTemaCapacita == it.codTemaCapacita)) // curso de perfil
                        {
                            if (it.intentos > 0 || it.nota>0)
                            {
                                if (it.notabase == 0) it.notabase =new decimal(0.001);
                                if ((it.nota * 100 / it.notabase) >= it.porcentaje) {
                                    it.estado = "0";
                                    it.certificado = true;
                                }
                                else it.estado = "1";
                            }
                            else it.estado = "2";
                        }
                        else 
                        {
                            it.estado = "3";
                            if (it.notabase == 0) it.notabase = new decimal(0.001);
                            if ((it.nota * 100 / it.notabase) >= it.porcentaje) it.certificado = true;
                        }
                        it.estado = EstadosCurso.data.Where(i => i.CodTipo.Equals(it.estado)).Select(e => e.Descripcion).FirstOrDefault();
                        GeneralVM.data.Add(it);
                    }
                }
                GeneralVM.count = GeneralVM.data.Count;
                return GeneralVM;
            }           
        }
    }
}