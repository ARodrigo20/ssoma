using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.DTOs;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.VMs;
using Hsec.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries
{
    public class GetCursosDisponiblesQuery : IRequest<GetCursosDisponiblesVM>
    {
        public string codPersona { get; set; }
        public string codTema { get; set; }
        public class GetCursosDisponiblesQueryHandler : IRequestHandler<GetCursosDisponiblesQuery, GetCursosDisponiblesVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;
            
            public GetCursosDisponiblesQueryHandler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }
            public async Task<GetCursosDisponiblesVM> Handle(GetCursosDisponiblesQuery request, CancellationToken cancellationToken)
            {
                GetCursosDisponiblesVM GeneralVM = new GetCursosDisponiblesVM();
                string codPersona = request.codPersona;
                string codTema = request.codTema;
                var dateActual = DateTime.Now;
                var cursosdasboard = (
                                      from cursos in _context.TCurso.Where(i => i.Estado)
                                          //join expo in _context.TExpositor on cursos.CodCurso equals expo.CodCurso
                                      join tema in _context.TTemaCapacitacion.Where(i => i.Estado) on cursos.CodTemaCapacita equals tema.CodTemaCapacita
                                      into lista
                                      from list in lista.DefaultIfEmpty()
                                      where (cursos.Estado && list.Estado && cursos.CodTemaCapacita == codTema && ((cursos.FechaInicio.Date >= dateActual.Date) || (cursos.FechaInicio.Date <= dateActual.Date && dateActual.Date <= cursos.FechaFin.Date)))
                                      select new GetCursosDisponiblesDTO
                                      {
                                          codCurso = cursos.CodCurso,
                                          tema = list.Descripcion,
                                          fechaFinal = cursos.FechaFin,
                                          codVigenciaCapacita = cursos.CodVigenciaCapacita,
                                          vigencia = cursos.Vigencia,
                                          fechaIni = cursos.FechaInicio,
                                          horaInicio = cursos.FechaInicio.ToString("hh", CultureInfo.InvariantCulture) + ":" + cursos.FechaInicio.ToString("mm", CultureInfo.InvariantCulture) + " " + cursos.FechaInicio.ToString("tt", CultureInfo.InvariantCulture),
                                          tipo = list.CodTipoTema,
                                          puntajeTotal = cursos.PuntajeTotal,
                                          capacidad = cursos.Capacidad,
                                          codEmpresaCap = cursos.CodEmpCapacita,
                                          codTemaCapacita = cursos.CodTemaCapacita,
                                          enlace = cursos.Enlace,
                                          expositores = (from exp in cursos.Expositores
                                                         where exp.Estado && exp.CodCurso == cursos.CodCurso
                                                         select new GetCursoDisponibleExpositorDTO()
                                                         {
                                                             codPersona = exp.CodPersona,
                                                             codTemaCapacita = exp.CodCurso,
                                                             tipo = exp.Tipo
                                                         }).ToList()
                                      }).ToList();

                //var noRepeat = cursosdasboard.GroupBy(x => x.codCurso).Select(y => y.First()).ToList();

                if (cursosdasboard.Count > 0)
                {
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
                                    respPart = await _mediator.Send(new GetPersonaQuery() { CodPersona = codPersonaExp });
                                }

                                if (!string.IsNullOrEmpty(respPart.Nombres))
                                {
                                    it.nombre = respPart.ApellidoPaterno + " " + respPart.ApellidoMaterno + " " + respPart.Nombres;
                                }
                            }
                        }
                        item.disponibles = Disponibles;
                        item.inscrito = Inscrito;
                    }

                    foreach (var it in cursosdasboard)
                    {
                        DateTime fechFin = new DateTime();
                        var dateTime = it.fechaIni;
                        if (it.codVigenciaCapacita != "5")
                        {
                            //var dateTime = new DateTime(it.fechaIni.Year, it.fechaIni.Month, 1);
                            
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

                        DateTime fechaFinal = fechFin;
                        //DateTime fechaActual = DateTime.Today;
                        TimeSpan diferenciaFechas = fechaFinal.Subtract(dateTime);

                        int years = (int)(diferenciaFechas.Days / 365.25);
                        int months = (int)(((diferenciaFechas.Days / 365.25) - years) * 12);
                        int dias = diferenciaFechas.Days;
                        int weeks = (dias / 7);

                        it.fechaVen = fechFin;

                        if (Math.Abs(dias) < 7)
                        {
                            string duracion = dias + " " + "dia";

                            if (Math.Abs(dias) != 1)
                            {
                                it.duracion = duracion + "s";
                            }
                            else
                            {
                                it.duracion = duracion;
                            }
                        }

                        else if (Math.Abs(dias) >= 7 && Math.Abs(dias) < 31)
                        {
                            string duracion = weeks + " " + "semana";

                            if (Math.Abs(weeks) != 1)
                            {
                                it.duracion = duracion + "s";
                            }
                            else
                            {
                                it.duracion = duracion;
                            }

                        }

                        else if (Math.Abs(dias) >= 31 && Math.Abs(dias) < 365)
                        {
                            //int years = (int)(yearsOld.TotalDays / 365.25);
                            //int months = (int)(((fechFin.TotalDays / 365.25) - years) * 12);
                            string duracion = months + " " + "mes";

                            if (Math.Abs(months) != 1)
                            {
                                it.duracion = duracion + "es";
                            }
                            else
                            {
                                it.duracion = duracion;
                            }
                        }

                        else if (Math.Abs(dias) >= 365)
                        {
                            string duracion = years + " " + "año";

                            if (Math.Abs(years) != 1)
                            {
                                it.duracion = duracion + "s";
                            }
                            else
                            {
                                it.duracion = duracion;
                            }
                        }
                        //var codPer = it.expoCodPersona;
                        //var nomApPer = await _perNomApe.RequestNombApellidos(codPer);
                        //it.expoName = nomApPer.nombres + " " + nomApPer.apellidoPaterno + " " + nomApPer.apellidoMaterno;
                        GeneralVM.data.Add(it);
                    }
                    GeneralVM.count = GeneralVM.data.Count;
                }
                return GeneralVM;
            }
        }
    }
}