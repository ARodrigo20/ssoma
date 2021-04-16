using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries
{
    public class GetCursoProgramacionRuleAllQuery : IRequest<GetCursoProgramacionRuleResponseAllVM>
    {
        public class GetCursoProgramacionRuleResponseAllVMHandler : IRequestHandler<GetCursoProgramacionRuleAllQuery, GetCursoProgramacionRuleResponseAllVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCursoProgramacionRuleResponseAllVMHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetCursoProgramacionRuleResponseAllVM> Handle(GetCursoProgramacionRuleAllQuery request, CancellationToken cancellationToken)
            {
                var objetos = _context.TCursoRules.Where(i => i.Estado).OrderByDescending(i => i.RecurrenceID)
                .ProjectTo<GetCursoProgramacionRuleResponseAllDto>(_mapper.ConfigurationProvider)
                .ToList();

                var TemaCap= _context.TTemaCapacitacion.Where(i => i.Estado).ToList();
                foreach (var item in objetos) {
                    //item.title = TemaCap.FirstOrDefault(i => i.CodTemaCapacita == item.codTemaCapacita).Descripcion;
                    var _tema = TemaCap.FirstOrDefault(i => i.CodTemaCapacita == item.codTemaCapacita);
                    if(_tema != null)
                    {
                        item.title = _tema.Descripcion;
                    }
                    DateTimeOffset formato = new DateTimeOffset(item.start.Year, item.start.Month, item.start.Day, item.start.Hour, item.start.Minute, item.start.Second,TimeSpan.Zero);

                    var primParte = formato.ToUniversalTime().ToString("yyyy''MM''dd'T'HH''mm''ss'''Z'");

                    item.rrule = "DTSTART:" + primParte + "\nRRULE:" + item.recurrenceRule;
                    item.backgroundColor = "#FF7373";
                    item.borderColor = "red";
                    item.textColor = "white";
                    if (item.end >= item.start)
                    {
                        if (item.start.Hour == item.end.Hour && item.start.Minute == item.end.Minute && item.start.Second == item.end.Second)
                        {
                            item.duration = "00:00";
                        }
                        else
                        {
                            int horas = item.end.Hour - item.start.Hour;
                            int minutos = item.end.Minute - item.start.Minute;
                            string formatoFinal = "";
                            if (horas < 10)
                            {
                                formatoFinal += "0" + horas;
                            }

                            else
                            {
                                formatoFinal += horas;
                            }

                            formatoFinal += ":";
                            if (minutos < 10)
                            {
                                formatoFinal += "0" + minutos;
                            }
                            else
                            {
                                formatoFinal += minutos;
                            }

                            item.duration = formatoFinal;
                        }
                    }
                }

                GetCursoProgramacionRuleResponseAllVM ViewModelResp = new GetCursoProgramacionRuleResponseAllVM();
                                             
                ViewModelResp.data = objetos;
                ViewModelResp.count = objetos.Count;

                return ViewModelResp;
            }
        }
    }
}
