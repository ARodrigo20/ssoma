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

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries
{
    public class GetCursoProgramacionFechasQuery : IRequest<GetCursoProgramacionFechasVM>
    {
        public DateTime fechaInicio { get; set; } // fecInicio
        public DateTime fechaFin { get; set; } // fecFin
        public class GetCursoProgramacionFechasQueryHandler : IRequestHandler<GetCursoProgramacionFechasQuery, GetCursoProgramacionFechasVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCursoProgramacionFechasQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetCursoProgramacionFechasVM> Handle(GetCursoProgramacionFechasQuery request, CancellationToken cancellationToken)
            {
                GetCursoProgramacionFechasVM cursoProgramacion = new GetCursoProgramacionFechasVM();

                var fechaIni = request.fechaInicio;
                var fechaFin = request.fechaFin;

                var LISTA = (from curso in _context.TCurso.Where(i => i.Estado)
                             join temaCap in _context.TTemaCapacitacion.Where(i => i.Estado) on curso.CodTemaCapacita equals temaCap.CodTemaCapacita into lista
                             from list in lista.DefaultIfEmpty()
                             where (
                             //((fechaIni.Date <=  curso.FechaInicio.Date && curso.FechaFin.Date <= fechaFin.Date))
                             curso.Estado && list.Estado &&
                            (fechaIni.Date <= curso.FechaInicio.Date && curso.FechaInicio.Date <= fechaFin.Date) || (fechaIni.Date <= curso.FechaFin.Date && curso.FechaFin.Date <= fechaFin.Date)
                            )
                             select new GetCursoProgramacionFechasDto()
                             {
                                 id = curso.CodCurso,
                                 title = list.Descripcion,
                                 codTemaCapacita = curso.CodTemaCapacita,
                                 start = curso.FechaInicio,
                                 end = curso.FechaFin,
                                 duration= curso.Duracion,
                                 recurrenceId = curso.RecurrenceID,
                                 backgroundColor = "#31ABFF",
                                 borderColor = "blue",
                                 textColor = "white"
                             }).OrderByDescending(i => i.id).ToList();
                cursoProgramacion.data= LISTA;
                cursoProgramacion.count = cursoProgramacion.data.Count();
                return cursoProgramacion;
                //foreach (var item in LISTA)
                //{
                //    if (item.end.Date >= item.start.Date)
                //    {
                //        if (item.start.Hour == item.end.Hour && item.start.Minute == item.end.Minute && item.start.Second == item.end.Second)
                //        {
                //            item.duration = "00:00";
                //        }
                //        else
                //        {
                //            int horas = item.end.Hour - item.start.Hour;
                //            int minutos = item.end.Minute - item.start.Minute;
                //            string formatoFinal = "";
                //            if (horas < 10)
                //            {
                //                formatoFinal += "0" + horas;
                //            }

                //            else
                //            {
                //                formatoFinal += horas;
                //            }

                //            formatoFinal += ":";
                //            if (minutos < 10)
                //            {
                //                formatoFinal += "0" + minutos;
                //            }
                //            else
                //            {
                //                formatoFinal += minutos;
                //            }

                //            item.duration = formatoFinal;
                //        }
                //        cursoProgramacion.data.Add(item);
                //    }
                //}

            }
        }
    }
}