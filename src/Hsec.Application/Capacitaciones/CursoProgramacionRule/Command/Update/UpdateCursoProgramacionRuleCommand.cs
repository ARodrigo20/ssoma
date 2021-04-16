using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Update.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Update
{
    public class UpdateCursoProgramacionRuleCommand : IRequest<Unit>
    {

        public UpdateCursoProgramacionRuleVM VM { get; set; }
        public class UpdateCursoProgramacionRuleCommandHandler : IRequestHandler<UpdateCursoProgramacionRuleCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            public UpdateCursoProgramacionRuleCommandHandler(IApplicationDbContext context, IMediator mediator)
            {
                this._context = context;
                this._mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateCursoProgramacionRuleCommand request, CancellationToken cancellationToken)
            {
                var item = _context.TCursoRules.FirstOrDefault(i => i.RecurrenceID == request.VM.id);
                var VM = request.VM;

                if (item != null) {
                    item.TCurso = null;
                    //item.CodCurso = VM.id;                                  
                    var tema = _context.TTemaCapacitacion.FirstOrDefault(i => i.CodTemaCapacita == item.CodTemaCapacita && i.Estado);
                    tema.Descripcion = VM.title;
                    item.CodTemaCapacita = VM.codTemaCapacita;
                    //if (VM.end > VM.start)
                    //{
                    //    item.FechaInicioRule = VM.start;
                    //    item.FechaFinRule = VM.end;

                    //    DateTimeOffset formato = new DateTimeOffset(VM.start.Year, VM.start.Month, VM.start.Day, VM.start.Hour, VM.start.Minute, VM.start.Second,
                    //                         TimeSpan.Zero);

                    //    var primParte = formato.ToUniversalTime()
                    //            .ToString("yyyy''MM''dd'T'HH''mm''ss'''Z'");

                    //    item.rRule = "DTSTART:" + primParte + "\nRRULE:" + item.RecurrenceRule;
                    //}

                    if (VM.fechaFin > VM.fechaInicio)
                    {
                        item.FechaInicio = VM.fechaInicio;
                        item.FechaFin = VM.fechaFin;
                    }

                    item.RecurrenceRule = VM.recurrenceRule;
                    item.TipoRecurrenceRule = VM.tipoRecurrenceRule;

                    if (!String.IsNullOrEmpty(VM.recurrenceException))
                    {
                        item.RecurrenceException = VM.recurrenceException;
                    }

                    //item.Repetir = VM.repetir;
                    item.CodTipoTema = VM.codTipoTema;
                    item.CodAreaCapacita = VM.codAreaCapacita;
                    //item.CodHha = VM.codHha;
                    item.CodEmpCapacita = VM.codEmpCapacita;
                    item.PuntajeTotal = VM.puntajeTotal;
                    item.PorcAprobacion = VM.porcAprobacion;
                    item.CodLugarCapacita = VM.codLugarCapacita;
                    item.CodSala = VM.codSala;
                    item.Capacidad = VM.capacidad;
                    item.Vigencia = VM.vigencia;
                    item.CodVigenciaCapacita = VM.codVigenciaCapacita;

                    item.FechaLimite = VM.fechaLimite;
                    //item.DescripcionLugar = VM.descripcionLugar;
                   // item.TipoVigencia = VM.tipoVigencia;

                    _context.TCursoRules.Update(item);

                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
