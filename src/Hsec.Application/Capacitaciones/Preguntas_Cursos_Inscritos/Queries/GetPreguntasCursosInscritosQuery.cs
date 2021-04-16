using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Inscritos.Queries.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.DTOs;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Inscritos.Queries.DTOs;
using System.Globalization;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs;
using Hsec.Application.General.Maestro.Queries.GetTablaQuery;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Capacitaciones.Preguntas_Cursos_Inscritos.Queries
{
    public class GetPreguntasCursosInscritosQuery : IRequest<GetPreguntasCursosInscritosVM>
    {
        public string codPersona { get; set; }
        public class GetMisCapacitacionesQueryHandler : IRequestHandler<GetPreguntasCursosInscritosQuery, GetPreguntasCursosInscritosVM>
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
            public async Task<GetPreguntasCursosInscritosVM> Handle(GetPreguntasCursosInscritosQuery request, CancellationToken cancellationToken)
            {
                GetPreguntasCursosInscritosVM GeneralVM = new GetPreguntasCursosInscritosVM();
                //utilizar el servicio adicional!!
                var dateActual = DateTime.Now;
                string codPersona = request.codPersona;
                //var val = await _jerPos.JerarquiaPersonaPosicion(codPersona);                
                //var codPosicion = val.count > 0 ? val.data[0].data + "" : "x_x";

                var cursosdasboard = (from part in _context.TParticipantes.Where(i => i.Estado)
                                      join cursos in _context.TCurso.Where(i => i.Estado) on part.CodCurso equals cursos.CodCurso
                                      join tema in _context.TTemaCapacitacion.Where(i => i.Estado) on cursos.CodTemaCapacita equals tema.CodTemaCapacita
                                      into lista
                                      from list in lista.DefaultIfEmpty()
                                      where (part.Estado && cursos.Estado && list.Estado && part.CodPersona == codPersona && ((cursos.FechaInicio.Date >= dateActual.Date) || (cursos.FechaInicio.Date <= dateActual.Date && dateActual.Date <= cursos.FechaFin.Date)))
                                      select new GetPreguntasCursosInscritosDTO
                                      {
                                          codCurso = cursos.CodCurso,
                                          tema = list.Descripcion,
                                          fechaFinal = cursos.FechaFin,
                                          codVigenciaCapacita = cursos.CodVigenciaCapacita,
                                          vigencia = cursos.Vigencia,
                                          fechIni = cursos.FechaInicio,     
                                          enlace= cursos.Enlace,
                                          online= cursos.Online,
                                          duracion= cursos.Duracion,
                                          intentos= part.Intentos,
                                          horaFin = cursos.FechaFin.ToString("hh", CultureInfo.InvariantCulture) + ":" + cursos.FechaFin.ToString("mm", CultureInfo.InvariantCulture) + " " + cursos.FechaFin.ToString("tt", CultureInfo.InvariantCulture),
                                          codTemaCapacita = cursos.CodTemaCapacita,
                                          horaInicio = cursos.FechaInicio.ToString("hh", CultureInfo.InvariantCulture) + ":" + cursos.FechaInicio.ToString("mm", CultureInfo.InvariantCulture) + " " + cursos.FechaInicio.ToString("tt", CultureInfo.InvariantCulture),
                                          tipo = list.CodTipoTema,
                                          puntajeTotal = cursos.PuntajeTotal,
                                          capacidad = cursos.Capacidad,
                                          codEmpresaCap = cursos.CodEmpCapacita,
                                          expositores = (from exp in cursos.Expositores
                                                         where exp.Estado && exp.CodCurso == cursos.CodCurso
                                                         select new GetCursoDisponibleExpositorDTO()
                                                         {
                                                             codPersona = exp.CodPersona,
                                                             codTemaCapacita = exp.CodCurso,
                                                             tipo = exp.Tipo
                                                         }).ToList()

                                      }).OrderBy(i => i.fechIni).ToList();

                // var distinctItems = cursosdasboard.OrderByDescending(i => i.fechIni).GroupBy(x => x.codTemaCapacita).Select(y => y.First()).ToList();

                //IList<GetPreguntasCursosInscritosDTO> listaCursosFiltrados = new List<GetPreguntasCursosInscritosDTO>();
                foreach (var item in cursosdasboard)
                {
                    if ((item.fechIni.Date <= dateActual.Date && dateActual.Date <= item.fechaFinal.Date))
                    {
                        item.estadoCurso = true;
                    }
                    else
                    {
                        item.estadoCurso = false;
                    }
                }

                if (cursosdasboard.Count > 0)
                {
                    //List<MaestroDTO> esmpresas = await _maestroService.GetMaestroTabla("EmpresaCapacita");
                    TablaVM esmpresas = await _mediator.Send(new GetTablaQuery() { CodTabla = "EmpresaCapacita" });
                    //List<MaestroDTO> TipoVigencia = await _maestroService.GetMaestroTabla("VigenciaCapacita");
                    TablaVM TipoVigencia = await _mediator.Send(new GetTablaQuery() { CodTabla = "VigenciaCapacita" });
                    foreach (var item in cursosdasboard)
                    {

                        var Participantes = _context.TParticipantes.Where(i => i.CodCurso == item.codCurso && i.Estado);
                        var Disponibles = item.capacidad - Participantes.Count();
                        var Inscrito = Participantes.Any(i => i.CodPersona == codPersona);
                        //GetResponseNombApVM respPart;
                        PersonaVM respPart;

                        if (item.expositores.Count > 0)
                        {
                            foreach (var it in item.expositores)
                            {
                                //respPart = new GetResponseNombApVM();
                                respPart = new PersonaVM();
                                var codPersonaExp = it.codPersona;
                                if (!string.IsNullOrEmpty(codPersonaExp))
                                {
                                    //respPart = await _perNomApe.RequestNombApellidos(codPersonaExp);
                                    respPart = await _mediator.Send(new GetPersonaQuery() { CodPersona = codPersona });
                                }

                                if (!string.IsNullOrEmpty(respPart.Nombres))
                                {
                                    it.nombre = respPart.ApellidoPaterno + " " + respPart.ApellidoMaterno + " " + respPart.Nombres;
                                }
                            }
                        }
                        item.EmpresaCap = esmpresas.data.Where(i => i.CodTipo.Equals(item.codEmpresaCap)).Select(e => e.Descripcion).FirstOrDefault();
                        item.disponibles = Disponibles;
                        item.inscrito = Inscrito;
                        item.vigenciatxt = item.vigencia + " " + TipoVigencia.data.Where(i => i.CodTipo.Equals(item.codVigenciaCapacita)).Select(e => e.Descripcion).FirstOrDefault();
                    }

               //     foreach (var it in cursosdasboard)
                   
                        //DateTime fechFin = new DateTime();
                        //if (it.codVigenciaCapacita != "5")
                        //{
                        //    //var dateTime = new DateTime(it.fechaIni.Year, it.fechaIni.Month, 1);
                        //    var dateTime = it.fechIni;
                        //    DateTime dateADD = new DateTime();
                        //    if ((it.codVigenciaCapacita == "1"))
                        //    {
                        //        dateADD = dateTime.AddDays(int.Parse(it.vigencia.ToString()));
                        //    }
                        //    else if (it.codVigenciaCapacita == "2")
                        //    {
                        //        dateADD = dateTime.AddDays((7) * (int.Parse(it.vigencia.ToString())));
                        //    }
                        //    else if (it.codVigenciaCapacita == "3")
                        //    {
                        //        dateADD = dateTime.AddMonths(int.Parse(it.vigencia.ToString()));
                        //    }
                        //    else if (it.codVigenciaCapacita == "4")
                        //    {
                        //        dateADD = dateTime.AddYears(int.Parse(it.vigencia.ToString()));
                        //    }
                        //    fechFin = dateADD;
                        //}
                        //else
                        //{
                        //    fechFin = it.fechaFinal;
                        //}

                        //var fechaIni = it.fechIni;
                        //DateTime fechaFinal = fechFin;
                        ////DateTime fechaActual = DateTime.Today;
                        //TimeSpan diferenciaFechas = fechaFinal.Subtract(fechaIni);

                        //int years = (int)(diferenciaFechas.Days / 365.25);
                        //int months = (int)(((diferenciaFechas.Days / 365.25) - years) * 12);
                        //int dias = diferenciaFechas.Days;
                        //int weeks = (dias / 7);

                        //it.fechaVen = fechFin;
                        //if (dias > 0)
                        //{
                        //    it.vencimiento = true;
                        //}
                        //else
                        //{
                        //    it.vencimiento = false;
                        //}
                        
                        //else if (Math.Abs(dias) < 7)
                        //{
                        //    string duracion = dias + " " + "dia";

                        //    if (Math.Abs(dias) != 1)
                        //    {
                        //        it.duracion = duracion + "s";
                        //    }
                        //    else
                        //    {
                        //        it.duracion = duracion;
                        //    }
                        //}

                        //else if (Math.Abs(dias) >= 7 && Math.Abs(dias) < 31)
                        //{
                        //    string duracion = weeks + " " + "semana";

                        //    if (Math.Abs(weeks) != 1)
                        //    {
                        //        it.duracion = duracion + "s";
                        //    }
                        //    else
                        //    {
                        //        it.duracion = duracion;
                        //    }

                        //}

                        //else if (Math.Abs(dias) >= 31 && Math.Abs(dias) < 365)
                        //{
                        //    //int years = (int)(yearsOld.TotalDays / 365.25);
                        //    //int months = (int)(((fechFin.TotalDays / 365.25) - years) * 12);
                        //    string duracion = months + " " + "mes";

                        //    if (Math.Abs(months) != 1)
                        //    {
                        //        it.duracion = duracion + "es";
                        //    }
                        //    else
                        //    {
                        //        it.duracion = duracion;
                        //    }
                        //}

                        //else if (Math.Abs(dias) >= 365)
                        //{
                        //    string duracion = years + " " + "año";

                        //    if (Math.Abs(years) != 1)
                        //    {
                        //        it.duracion = duracion + "s";
                        //    }
                        //    else
                        //    {
                        //        it.duracion = duracion;
                        //    }
                        //}
                        
                        //if (it.n) {
                        //    GeneralVM.data.Add(it);
                        //}
                    //}
                }
                GeneralVM.data = cursosdasboard;
                GeneralVM.count = GeneralVM.data.Count;
                return GeneralVM;
            }
        }
    }
}