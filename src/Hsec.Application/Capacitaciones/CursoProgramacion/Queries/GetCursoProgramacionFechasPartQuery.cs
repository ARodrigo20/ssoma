using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs;
using MediatR;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries
{
    public class GetCursoProgramacionFechasPartQuery : IRequest<GetCursoProgramacionFechasPartVM>
    {
        public DateTime fechaInicio { get; set; } // fecInicio
        public DateTime fechaFin { get; set; } // fecFin
        public class GetCursoProgramacionFechasPartQueryHandler : IRequestHandler<GetCursoProgramacionFechasPartQuery, GetCursoProgramacionFechasPartVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public readonly IMediator _mediator;

            public GetCursoProgramacionFechasPartQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<GetCursoProgramacionFechasPartVM> Handle(GetCursoProgramacionFechasPartQuery request, CancellationToken cancellationToken)
            {
                GetCursoProgramacionFechasPartVM cursoProgramacion = new GetCursoProgramacionFechasPartVM();

                var fechaIni = request.fechaInicio;
                var fechaFin = request.fechaFin;

                var LISTA = (from curso in _context.TCurso.Where(i => i.Estado)
                             join temaCap in _context.TTemaCapacitacion.Where(i => i.Estado) on curso.CodTemaCapacita equals temaCap.CodTemaCapacita into lista
                             from list in lista.DefaultIfEmpty()
                             where (
                             //(fechaIni.Date <= curso.FechaInicio.Date && curso.FechaFin.Date <= fechaFin.Date)
                             curso.Estado && list.Estado &&
                             (fechaIni.Date <= curso.FechaInicio.Date && curso.FechaInicio.Date <= fechaFin.Date) || (fechaIni.Date <= curso.FechaFin.Date && curso.FechaFin.Date <= fechaFin.Date)
                             )
                             select new GetCursoProgramacionFechasPartDto()
                             {
                                 id = curso.CodCurso,
                                 recurrenceID = curso.RecurrenceID,
                                 title = list.Descripcion,
                                 codTemaCapacita = curso.CodTemaCapacita,
                                 start = curso.FechaInicio,
                                 duration=curso.Duracion,
                                 end = curso.FechaFin,
                                 participantes = (from part in curso.Participantes.Where(i => i.Estado)
                                                  select new GetCursoProgramacionParticipanteDto()
                                                  {
                                                      codPersona = part.CodPersona,
                                                      codCurso = part.CodCurso,
                                                      nota = (!string.IsNullOrEmpty(part.Nota.ToString()) ? part.Nota.ToString() : ""),
                                                      tipo = part.Tipo                                                     
                                                  }).ToList(),
                             }).ToList();

                foreach (var item in LISTA)
                {
                    var part = item.participantes;
                    if (part.Count > 0) {
                        foreach (var it in part) {
                            string codPersona = it.codPersona;
                            if (!string.IsNullOrEmpty(codPersona)) {
                                //GetResponseNombApPartVM vm = await _personasService.RequestNombApellidosPart(codPersona);
                                var vm = await _mediator.Send(new GetPersonaQuery() { CodPersona = codPersona });
                                it.nombre = vm.ApellidoPaterno + " " + vm.ApellidoMaterno + ", " + vm.Nombres;
                                //it.dni = vm.nroDocumento;
                                //it.empresa = vm.empresa;                                
                            }                            
                        }
                    }

                    //if (item.end >= item.start)
                    //{
                    //    if (item.start.Hour == item.end.Hour && item.start.Minute == item.end.Minute && item.start.Second == item.end.Second)
                    //    {
                    //        item.duration = "00:00";
                    //    }
                    //    else
                    //    {
                    //        int horas = item.end.Hour - item.start.Hour;
                    //        int minutos = item.end.Minute - item.start.Minute;
                    //        string formatoFinal = "";
                    //        if (horas < 10) {
                    //            formatoFinal += "0" + horas;
                    //        }

                    //        else {
                    //            formatoFinal += horas;
                    //        }

                    //        formatoFinal += ":";
                    //        if (minutos < 10) {
                    //            formatoFinal += "0" + minutos;
                    //        }
                    //        else {
                    //            formatoFinal += minutos;
                    //        }

                    //        item.duration = formatoFinal;
                    //    }
                    //}                    
                    cursoProgramacion.data.Add(item);
                }
                cursoProgramacion.count = cursoProgramacion.data.Count();
                return cursoProgramacion;
            }
        }
    }
}