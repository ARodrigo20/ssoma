using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Create.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Create.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Create
{
    public class CreateCursoProgramacionRuleCommand : IRequest<Unit>
    {
        public CreateCursoProgramacionRuleDto VM { get; set; }
        public class CreateCursoProgramacionRuleCommandHandler : IRequestHandler<CreateCursoProgramacionRuleCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreateCursoProgramacionRuleCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(CreateCursoProgramacionRuleCommand request, CancellationToken cancellationToken)
            {                
                var item = request.VM;

                if (item.end > item.start)
                {
                    var numReg = _context.TCursoRules.Count();
                    var ultNum = 1;
                    if (numReg > 0)
                    {
                        ultNum = int.Parse(_context.TCursoRules.Max(i => i.RecurrenceID)) + 1;
                    }

                    var numDig = "000000";
                    TCursoRules cursoRule;

                    cursoRule = new TCursoRules();
                    cursoRule.TCurso = null;
                    //cursoRule.CodCurso = item.id;
                    cursoRule.CodTemaCapacita = item.codTemaCapacita;
                    cursoRule.TipoRecurrenceRule = item.tipoRecurrenceRule;
                    cursoRule.CodTipoTema = item.codTipoTema;

                    //cursoRule.FechaInicioRule = item.start;
                    //cursoRule.FechaFinRule = item.end;

                    //DateTimeOffset formato = new DateTimeOffset(item.start.Year, item.start.Month, item.start.Day, item.start.Hour, item.start.Minute, item.start.Second,
                    //                         TimeSpan.Zero);

                    //var primParte = formato.ToUniversalTime().ToString("yyyy''MM''dd'T'HH''mm''ss'''Z'");
                    //cursoRule.rRule = "DTSTART:" + primParte + "\nRRULE:FREQ=" + item.recurrenceRule;

                    if (item.fechaFin > item.fechaInicio)
                    {
                        cursoRule.FechaInicio = item.fechaInicio;
                        cursoRule.FechaFin = item.fechaFin;
                    }

                    cursoRule.RecurrenceRule = item.recurrenceRule;
                    cursoRule.RecurrenceException = item.recurrenceException;
                    //cursoRule.Repetir = item.repetir;

                    cursoRule.CodAreaCapacita = item.codAreaCapacita;
                    //cursoRule.CodHha = item.codHha;
                    cursoRule.CodEmpCapacita = item.codEmpCapacita;
                    cursoRule.PuntajeTotal = item.puntajeTotal;
                    cursoRule.PorcAprobacion = item.porcAprobacion;
                    cursoRule.CodLugarCapacita = item.codLugarCapacita;
                    cursoRule.CodSala = item.codSala;
                    cursoRule.Capacidad = item.capacidad;
                    cursoRule.Vigencia = item.vigencia;
                    cursoRule.CodVigenciaCapacita = item.codVigenciaCapacita;

                    cursoRule.FechaLimite = item.fechaLimite;
                  //  cursoRule.DescripcionLugar = item.descripcionLugar;
                   // cursoRule.TipoVigencia = item.tipoVigencia;
                    cursoRule.Estado = true;

                    var codCursoRule = numDig.Substring(0, numDig.Length - ultNum.ToString().Length) + ultNum;
                    cursoRule.RecurrenceID = codCursoRule;
                    _context.TCursoRules.Add(cursoRule);

                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {                   
                    Console.Write("FECHAS START Y END INVALIDAS ... Recuerde END > START");
                }
                return Unit.Value;
            }
        }
    }
}
