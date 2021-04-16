using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Command.CreateCurso.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Command.CreateCurso
{
    public class CreateCursoPosicionCommand : IRequest<Unit>
    {
        public IList<CreateCursoPosicionVM> VM { get; set; }
        public class CreateCursoPosicionCommandHandler : IRequestHandler<CreateCursoPosicionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreateCursoPosicionCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(CreateCursoPosicionCommand request, CancellationToken cancellationToken)
            {
                var modelVM = request.VM;
                foreach (var item in modelVM) {
                    var perfilDB = _context.TPlanTema.FirstOrDefault(p => p.CodTemaCapacita.Equals(item.codTemaCapacita) && p.CodReferencia.Equals(item.codPosicion));
                    if (perfilDB == null)// insertar
                    {
                        TPlanTema cap = new TPlanTema();
                        //cap.TemaCapacitacion = tema;
                        cap.CodReferencia = item.codPosicion;
                        cap.Tipo = item.tipo;
                        cap.CodTemaCapacita = item.codTemaCapacita;
                        _context.TPlanTema.Add(cap);
                    }
                    else // update estado
                    {
                        perfilDB.Estado = item.estado;
                        _context.TPlanTema.Update(perfilDB);
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
