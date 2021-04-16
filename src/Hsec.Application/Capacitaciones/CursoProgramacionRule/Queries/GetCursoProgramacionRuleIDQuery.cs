using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.VMs;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries
{
    public class GetCursoProgramacionRuleIDQuery : IRequest<GetCursoProgramacionRuleVM>
    {
        public string recurrenceID { get; set; } //codigo

        public class GetCursoProgramacionRuleIDQueryHandler : IRequestHandler<GetCursoProgramacionRuleIDQuery, GetCursoProgramacionRuleVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCursoProgramacionRuleIDQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetCursoProgramacionRuleVM> Handle(GetCursoProgramacionRuleIDQuery request, CancellationToken cancellationToken)
            {
                GetCursoProgramacionRuleVM cursoProgramacion = new GetCursoProgramacionRuleVM();

                string recurrenceID = request.recurrenceID;              
                var LISTA = (from cursoRules in _context.TCursoRules.Where(i => i.Estado)
                             join temaCap in _context.TTemaCapacitacion.Where(i => i.Estado) on cursoRules.CodTemaCapacita equals temaCap.CodTemaCapacita into lista
                             from list in lista.DefaultIfEmpty()
                             where (cursoRules.Estado && list.Estado && cursoRules.RecurrenceID == recurrenceID)
                             select new GetCursoProgramacionRuleDto()
                             {
                                 recurrenceId = cursoRules.RecurrenceID,
                                 codTemaCapacita = cursoRules.CodTemaCapacita,
                                 start = cursoRules.FechaInicio,
                                 end = cursoRules.FechaFin,
                                 recurrenceException = cursoRules.RecurrenceException,
                                 recurrenceRule = cursoRules.RecurrenceRule,
                                 tipoRecurrenceRule = cursoRules.TipoRecurrenceRule,
                                 estado = cursoRules.Estado,
                                 id = cursoRules.RecurrenceID,
                                 title = list.Descripcion,
                                 rrule = "DTSTART:" + new DateTimeOffset(
                                     cursoRules.FechaInicio.Year,
                                     cursoRules.FechaInicio.Month,
                                     cursoRules.FechaInicio.Day,
                                     cursoRules.FechaInicio.Hour,
                                     cursoRules.FechaInicio.Minute,
                                     cursoRules.FechaInicio.Second,
                                     TimeSpan.Zero
                                     ).ToUniversalTime().ToString("yyyy''MM''dd'T'HH''mm''ss'''Z'") + "\nRRULE:" + cursoRules.RecurrenceRule,
                                 //repetir = cursoRules.Repetir,
                                 codTipoTema = cursoRules.CodTipoTema,
                                 codAreaCapacita = cursoRules.CodAreaCapacita,
                                 //codHha = cursoRules.CodHha,
                                 codEmpCapacita = cursoRules.CodEmpCapacita,
                                 puntajeTotal = cursoRules.PuntajeTotal,
                                 porcAprobacion = cursoRules.PorcAprobacion,
                                 codLugarCapacita = cursoRules.CodLugarCapacita,
                                 codSala = cursoRules.CodSala,
                                 capacidad = cursoRules.Capacidad,
                                 vigencia = cursoRules.Vigencia,
                                 codVigenciaCapacita = cursoRules.CodVigenciaCapacita,
                                 fechaInicio = cursoRules.FechaInicio,
                                 fechaFin = cursoRules.FechaFin,
                                 fechaLimite = cursoRules.FechaLimite,
                                 //descripcionLugar = cursoRules.DescripcionLugar,
                                 //tipoVigencia = cursoRules.TipoVigencia

                                 //rrule = cursoRules.FechaInicioRule + cursoRules.RecurrenceRule                                 
                             });

                foreach (var item in LISTA)
                {
                    cursoProgramacion.data.Add(item);
                }
                cursoProgramacion.count = cursoProgramacion.data.Count();
                return cursoProgramacion;
            }
        }
    }
}
