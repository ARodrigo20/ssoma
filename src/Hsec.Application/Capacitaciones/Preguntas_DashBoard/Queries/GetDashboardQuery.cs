using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries.VMs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries.DTOs;
using System;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries.VMs;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries.DTOs;
using System.Collections.Generic;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiasPersona;

namespace Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries
{
    public class GetDashboardQuery : IRequest<GetDashboardVM>
    {
        public string codPersona { get; set; }
        public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, GetDashboardVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public GetDashboardQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<GetDashboardVM> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
            {
                GetDashboardVM GeneralVM = new GetDashboardVM();
                //utilizar el servicio adicional!!
                GetCursoYCursoPosicionVM modelVM = new GetCursoYCursoPosicionVM();
                string codPersona = request.codPersona;
                //var val = await _jerPos.JerarquiaPersonaPosicion(codPersona);
                var val = await _mediator.Send(new JerarquiaPersonaGSQuery() { codPersona = codPersona });
                var codPosicion = val.count > 0 ? val.data[0].data + "" : "x_x";
                    //lista de cursos a mostrar LISt<string>
                    var cursosPerfil = (from plan in _context.TPlanTema.Where(i => i.Estado)
                                        join temaCap in _context.TTemaCapacitacion.Where(i => i.Estado) on plan.CodTemaCapacita equals temaCap.CodTemaCapacita
                                        into lista
                                        from list in lista.DefaultIfEmpty()
                                        where (plan.Estado && list.Estado && (codPersona == plan.CodReferencia || codPosicion.ToString() == plan.CodReferencia))
                                        select list.CodTemaCapacita ).ToList();

                    var cursosdasboard = (from part in _context.TParticipantes.Where(i => i.Estado)
                                          join cursos in _context.TCurso.Where(i => i.Estado) on part.CodCurso equals cursos.CodCurso
                                          join tema   in _context.TTemaCapacitacion.Where(i => i.Estado) on cursos.CodTemaCapacita equals tema.CodTemaCapacita
                                          into lista
                                          from list in lista.DefaultIfEmpty()
                                          where (part.Estado && cursos.Estado && list.Estado && cursosPerfil.Contains(cursos.CodTemaCapacita) && part.CodPersona == codPersona && part.Evaluado && part.Nota != null && part.Nota != 0)
                                          select new GetDashboardDTO
                                          {
                                              codCurso = cursos.CodCurso,
                                              tema = list.Descripcion,
                                              fechaFinal = cursos.FechaFin,
                                              codVigenciaCapacita = cursos.CodVigenciaCapacita,
                                              vigencia = cursos.Vigencia,
                                              fechaIni = cursos.FechaInicio,
                                              codTemaCapacita = cursos.CodTemaCapacita,
                                              fechaVen=part.Modificado
                                          }).ToList();

                    var distinctItems = cursosdasboard.OrderByDescending(i => i.fechaIni).GroupBy(x => x.codTemaCapacita).Select(y => y.First()).ToList();

                    if (distinctItems.Count > 0)
                    {
                        foreach (var it in distinctItems)
                        {                           
                            var dateTime = it.fechaVen ?? it.fechaIni;
                            DateTime fechFin = new DateTime();
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

                            //DateTime fechaVen = fechFin;
                            DateTime fechaFinal = fechFin;
                            TimeSpan diferenciaFechas = fechaFinal.Subtract(dateTime);

                            int years = (int)(diferenciaFechas.Days / 365.25);
                            int months = (int)(((diferenciaFechas.Days / 365.25) - years) * 12);
                            int dias = diferenciaFechas.Days;
                            int weeks = (dias / 7);

                            it.fechaVen = fechFin;
                            if (dias > 0)
                            {
                                it.vencimiento = true;
                            }
                            else
                            {
                                it.vencimiento = false;
                            }

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
                            GeneralVM.data.Add(it);
                        }
                    }
                    GeneralVM.count = GeneralVM.data.Count;             
                return GeneralVM;
            }
        }
    }
}