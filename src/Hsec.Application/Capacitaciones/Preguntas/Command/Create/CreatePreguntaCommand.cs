using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Preguntas.Command.Create.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Preguntas.Command.Create
{
    public class CreatePreguntaCommand : IRequest<Unit>
    {
        public CreatePreguntaVM VM { get; set; }
        public class CreatePlanTemaCommandHandler : IRequestHandler<CreatePreguntaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreatePlanTemaCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(CreatePreguntaCommand request, CancellationToken cancellationToken)
            {
                var model = request.VM;
                TPreguntas preguntasEntity = new TPreguntas();
                preguntasEntity.CodCurso = model.codCurso;
                preguntasEntity.CodPregunta = model.codPregunta;
                preguntasEntity.Descripcion = model.descripcion;
                preguntasEntity.Tipo = model.tipo;
                preguntasEntity.Puntaje = model.puntaje;
                preguntasEntity.Respuesta = model.respuesta;

                TAlternativas alter;

                foreach (var alt in model.alternativas) {
                    alter = new TAlternativas();
                    alter.CodAlternativa = alt.codAlternativa;
                    alter.CodPregunta = alt.codPregunta;
                    alter.Descripcion = alt.descripcion;
                    preguntasEntity.Alternativas.Add(alter);
                }
                _context.TPreguntas.Add(preguntasEntity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}