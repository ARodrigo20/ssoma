using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using Hsec.Application.Common.Exceptions;

namespace Hsec.Application.General.JerarquiasPersona.Commands.UpdateJerarquiasPersona
{
   public class UpdateJerarquiaPersonaCommand : IRequest
    {
        public int CodPosicion { get; set; }
        public string CodPersona { get; set; }
        //public int CodTipoPersona { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Asignacion { get; set; }

        public class UpdateJerarquiasPersonaCommandHandler : IRequestHandler<UpdateJerarquiaPersonaCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateJerarquiasPersonaCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateJerarquiaPersonaCommand request, CancellationToken cancellationToken)
            {

                var entity = await _context.TJerarquiaPersona.FindAsync(request.CodPosicion, request.CodPersona);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Jerarquias), request.CodPosicion);
                }

                if (entity.Estado == true)
                {
                    entity.FechaInicio = request.FechaInicio;
                    entity.FechaFin = request.FechaFin;
                    entity.PosicionPrimaria = request.Asignacion;
                    //entity.CodTipoPersona = request.CodTipoPersona;
                    _context.TJerarquiaPersona.Update(entity);
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(true);
                }

                else
                {
                    throw new UpdateEstadoRegistroException(nameof(Jerarquias), entity.Estado);
                }
                return Unit.Value;
            }
        }
    }
}
