using AutoMapper;
using Hsec.Application.Common.EntityDTO;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.DTOs;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.VMs;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Inscritos.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiasPersona;
using Hsec.Application.General.Maestro.Queries.GetTablaQuery;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries
{
    public class GetCursosDisponiblesSinFiltroQuery : IRequest<CollectionResponse<GetCursosDisponiblesDTO>>
    {
        public string codPersona { get; set; }
        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }
        public class GetCursosDisponiblesSinFiltroQueryHandler : IRequestHandler<GetCursosDisponiblesSinFiltroQuery, CollectionResponse<GetCursosDisponiblesDTO>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetCursosDisponiblesSinFiltroQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<CollectionResponse<GetCursosDisponiblesDTO>> Handle(GetCursosDisponiblesSinFiltroQuery request, CancellationToken cancellationToken)
            {                
                var dateActual = DateTime.Now;
                //var JEraPer = await _jerPos.JerarquiaPersonaPosicion(request.codPersona);
                var JEraPer = await _mediator.Send(new JerarquiaPersonaGSQuery() { codPersona = request.codPersona });

                var CodPosicion = JEraPer.count > 0 ? JEraPer.data[0].data + "" : "x_x";
                CollectionResponse<GetCursosDisponiblesDTO> DataOut = new CollectionResponse<GetCursosDisponiblesDTO>();
                List<string> TemasPerfil = _context.TPlanTema.Where(p => p.CodReferencia.Equals(request.codPersona) || p.CodReferencia.Equals(CodPosicion)).Select(r => r.CodTemaCapacita).ToList();
                var cursosdasboard = (
                                      from cursos in _context.TCurso.Where(i => i.Estado)
                                          //join expo in _context.TExpositor on cursos.CodCurso equals expo.CodCurso
                                      join tema in _context.TTemaCapacitacion.Where(i => i.Estado) on cursos.CodTemaCapacita equals tema.CodTemaCapacita
                                      into lista
                                      from list in lista.DefaultIfEmpty()
                                      where (cursos.Estado && list.Estado && !TemasPerfil.Contains(cursos.CodTemaCapacita) && (cursos.FechaInicio.Date >= dateActual.Date) || (cursos.FechaInicio.Date <= dateActual.Date && dateActual.Date <= cursos.FechaFin.Date))
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
                                          duracion=cursos.Duracion,
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

                DataOut.Count = cursosdasboard.Count;
                DataOut.Data = cursosdasboard.Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio).Take(request.PaginaTamanio).ToList();
                
                if (DataOut.Count > 0)
                {
                    //List<MaestroDTO> esmpresas = await _maestroService.GetMaestroTabla("EmpresaCapacita");
                    TablaVM esmpresas = await _mediator.Send(new GetTablaQuery() { CodTabla = "EmpresaCapacita" });
                    //List<MaestroDTO> TipoVigencia = await _maestroService.GetMaestroTabla("VigenciaCapacita");
                    TablaVM TipoVigencia = await _mediator.Send(new GetTablaQuery() { CodTabla = "VigenciaCapacita" });
                    foreach (var item in DataOut.Data)
                    {
                        //var Participantes = _context.TParticipantes.Where(i => i.CodCurso == item.codCurso && i.Estado);
                        var Participantes = _context.TParticipantes.Where(i => i.Estado && i.CodCurso == item.codCurso).ToList();
                        var Disponibles = item.capacidad - Participantes.Count();
                        var Inscrito = Participantes.Any(i => i.CodPersona == request.codPersona);
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
                        item.EmpresaCap = esmpresas.data.Where(i => i.CodTipo.Equals(item.codEmpresaCap)).Select(e => e.Descripcion).FirstOrDefault();
                        item.inscrito = Inscrito;
                        item.vigenciatxt = item.vigencia + " " + TipoVigencia.data.Where(i => i.CodTipo.Equals(item.codVigenciaCapacita)).Select(e => e.Descripcion).FirstOrDefault();
                    }

                    //foreach (var it in DataOut.Data)
                    //{
                    //    DateTime fechFin = new DateTime();
                    //    var dateTime = it.fechaIni;
                    //    if (it.codVigenciaCapacita != "5")
                    //    {
                    //        //var dateTime = new DateTime(it.fechaIni.Year, it.fechaIni.Month, 1);

                    //        DateTime dateADD = new DateTime();
                    //        if ((it.codVigenciaCapacita == "1"))
                    //        {
                    //            dateADD = dateTime.AddDays(int.Parse(it.vigencia.ToString()));
                    //        }
                    //        else if (it.codVigenciaCapacita == "2")
                    //        {
                    //            dateADD = dateTime.AddDays((7) * (int.Parse(it.vigencia.ToString())));
                    //        }
                    //        else if (it.codVigenciaCapacita == "3")
                    //        {
                    //            dateADD = dateTime.AddMonths(int.Parse(it.vigencia.ToString()));
                    //        }
                    //        else if (it.codVigenciaCapacita == "4")
                    //        {
                    //            dateADD = dateTime.AddYears(int.Parse(it.vigencia.ToString()));
                    //        }
                    //        fechFin = dateADD;
                    //    }
                    //    else
                    //    {
                    //        fechFin = it.fechaFinal;
                    //    }

                    //    DateTime fechaFinal = fechFin;
                    //    //DateTime fechaActual = DateTime.Today;
                    //    TimeSpan diferenciaFechas = fechaFinal.Subtract(dateTime);

                    //    int years = (int)(diferenciaFechas.Days / 365.25);
                    //    int months = (int)(((diferenciaFechas.Days / 365.25) - years) * 12);
                    //    int dias = diferenciaFechas.Days;
                    //    int weeks = (dias / 7);

                    //    it.fechaVen = fechFin;

                    //    if (Math.Abs(dias) < 7)
                    //    {
                    //        string duracion = dias + " " + "dia";

                    //        if (Math.Abs(dias) != 1)
                    //        {
                    //            it.duracion = duracion + "s";
                    //        }
                    //        else
                    //        {
                    //            it.duracion = duracion;
                    //        }
                    //    }

                    //    else if (Math.Abs(dias) >= 7 && Math.Abs(dias) < 31)
                    //    {
                    //        string duracion = weeks + " " + "semana";

                    //        if (Math.Abs(weeks) != 1)
                    //        {
                    //            it.duracion = duracion + "s";
                    //        }
                    //        else
                    //        {
                    //            it.duracion = duracion;
                    //        }

                    //    }

                    //    else if (Math.Abs(dias) >= 31 && Math.Abs(dias) < 365)
                    //    {
                    //        //int years = (int)(yearsOld.TotalDays / 365.25);
                    //        //int months = (int)(((fechFin.TotalDays / 365.25) - years) * 12);
                    //        string duracion = months + " " + "mes";

                    //        if (Math.Abs(months) != 1)
                    //        {
                    //            it.duracion = duracion + "es";
                    //        }
                    //        else
                    //        {
                    //            it.duracion = duracion;
                    //        }
                    //    }

                    //    else if (Math.Abs(dias) >= 365)
                    //    {
                    //        string duracion = years + " " + "año";

                    //        if (Math.Abs(years) != 1)
                    //        {
                    //            it.duracion = duracion + "s";
                    //        }
                    //        else
                    //        {
                    //            it.duracion = duracion;
                    //        }
                    //    }
                    //    //var codPer = it.expoCodPersona;
                    //    //var nomApPer = await _perNomApe.RequestNombApellidos(codPer);
                    //    //it.expoName = nomApPer.nombres + " " + nomApPer.apellidoPaterno + " " + nomApPer.apellidoMaterno;
                    //    //DataOut.Data.data.Add(it);
                    //}
                    
                }
                return DataOut;
            }
        }
    }
}