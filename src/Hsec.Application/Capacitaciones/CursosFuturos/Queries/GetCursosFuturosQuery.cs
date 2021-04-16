using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursosFuturos.Queries.DTOs;
using Hsec.Application.Capacitaciones.CursosFuturos.Queries.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Capacitaciones.CursosFuturos.Queries
{
    public class GetCursosFuturosQuery : IRequest<GetCursosFuturosVM>
    {
        public string codTemaCapacita { get; set; }
        public class GetCursosFuturosQueryHandler : IRequestHandler<GetCursosFuturosQuery, GetCursosFuturosVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetCursosFuturosQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<GetCursosFuturosVM> Handle(GetCursosFuturosQuery request, CancellationToken cancellationToken)
            {
                GetCursosFuturosVM cursoProgramacion = new GetCursosFuturosVM();
                var codTemaCapacita = request.codTemaCapacita;

                var cursosTema = _context.TCurso.Where(i => i.CodTemaCapacita == codTemaCapacita && i.Estado);

                var objeto = cursosTema
               .ProjectTo<GetCursosFuturosDTO>(_mapper.ConfigurationProvider).ToList();

                var distinctItems = objeto.OrderByDescending(i => i.fechaInicio).GroupBy(x => x.codCurso).Select(y => y.First()).ToList();
                if (distinctItems.Count > 0)
                {
                    foreach (var item in distinctItems)
                    {
                        DateTime fechaVen = DateTime.Parse(item.fechaFin.ToString());
                        DateTime fechaActual = DateTime.Today;
                        TimeSpan diferenciaFechas = fechaVen.Subtract(fechaActual);

                        int years = (int)(diferenciaFechas.Days / 365.25);
                        int months = (int)(((diferenciaFechas.Days / 365.25) - years) * 12);
                        //int dias = diferenciaFechas.Days;
                        //int weeks = (dias / 7);
                        var fechIniNull = item.fechaInicio;
                        if (fechIniNull != null)
                        {
                            DateTime fechIni = (DateTime)(fechIniNull);
                            if ((fechIni.Date >= fechaActual.Date) || (fechIni.Date <= fechaActual.Date && fechaActual.Date <= fechaVen.Date))
                            {
                                if (item.participantes.Count > 0)
                                {
                                    foreach (var part in item.participantes)
                                    {
                                        //var model = await _personasService.RequestNombApellidosPart(part.codPersona);
                                        var model = await _mediator.Send(new GetPersonaQuery() { CodPersona = part.codPersona });
                                        if (model != null)
                                        {
                                            part.nombre = model.Nombres + " " + model.ApellidoPaterno + " " + model.ApellidoMaterno;
                                            //part.dni = model.nroDocumento;
                                            //part.empresa = model.empresa;
                                        }
                                    }
                                }

                                if (item.expositores.Count > 0)
                                {
                                    foreach (var exp in item.expositores)
                                    {
                                        //var model = await _personasService.RequestNombApellidos(exp.codPersona);
                                        var model = await _mediator.Send(new GetPersonaQuery() { CodPersona = exp.codPersona });
                                        if (model != null)
                                        {
                                            exp.nombre = model.Nombres + " " + model.ApellidoPaterno + " " + model.ApellidoMaterno;
                                        }
                                    }
                                }

                                if (months < 2)
                                {
                                    cursoProgramacion.data.Add(item);
                                }
                            }
                        }
                        else {
                            throw new ExceptionGeneral("FECHA INICIAL NULA !! NO ESTABLECIDA !! REVISE DICHA FECHA !!");                        
                        }
                    }
                }
                cursoProgramacion.count = cursoProgramacion.data.Count;
                return cursoProgramacion;
            }
        }
    }
}