using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Preguntas.Command.Delete
{
    public class DeletePreguntaCommand : IRequest
    {
        public int codPregunta { get; set; }
        public class DeletePreguntaCommandHandler : IRequestHandler<DeletePreguntaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeletePreguntaCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(DeletePreguntaCommand request, CancellationToken cancellationToken)
            {
                var busquedaPregunta = _context.TPreguntas.Include(i => i.Alternativas).FirstOrDefault(i => i.CodPregunta == request.codPregunta && i.Estado);

                if (busquedaPregunta != null)
                {
                    busquedaPregunta.Estado = false;
                    var alter = busquedaPregunta.Alternativas;
                    if (alter.Count > 0) {
                        foreach (var item in alter) {
                            item.Estado = false;
                            _context.TAlternativas.Update(item);
                        }                    
                    }
                    _context.TPreguntas.Update(busquedaPregunta);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
