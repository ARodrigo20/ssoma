using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Inscripcion_Personas.Command.Delete
{
    public class DeleteInscripcionCommand : IRequest<Unit>
    {
        public string codPersona { get; set; }
        public string codCurso { get; set; }

        public class CreateInscripcionCommandHandler : IRequestHandler<DeleteInscripcionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreateInscripcionCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(DeleteInscripcionCommand request, CancellationToken cancellationToken)
            {
                var codPersona = request.codPersona;
                var codCurso = request.codCurso;

                var participantes = _context.TParticipantes.FirstOrDefault(i => i.CodCurso == codCurso && i.CodPersona == codPersona && i.Estado);
                if (participantes != null)
                {
                    participantes.Estado = false;
                    _context.TParticipantes.Update(participantes);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                else {
                    throw new ExceptionGeneral("NO EXISTE !! DICHA PERSONA ASOCIADA A DICHO CURSO !!");
                }

                return Unit.Value;
            }
        }
    }
}