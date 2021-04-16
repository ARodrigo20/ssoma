using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Commands.UpdateCadenaAprobacion
{
    public class UpdateCadenaAprobacionCommand : IRequest<Unit>
    {
        public int CodAprobacion { get; set; }
        public string CodCadena { get; set; }
        public string CodCadenaNew { get; set; }        

        public class UpdateCadenaAprobacionCommandHandler : IRequestHandler<UpdateCadenaAprobacionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public UpdateCadenaAprobacionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCadenaAprobacionCommand request, CancellationToken cancellationToken)
            {
                var aprobacion = _context.TAprobacion.Find(request.CodAprobacion);
                aprobacion.CadenaAprobacion = aprobacion.CadenaAprobacion.Replace(request.CodCadena, request.CodCadenaNew);
                aprobacion.ProcesoAprobacion = aprobacion.ProcesoAprobacion.Replace(request.CodCadena, request.CodCadenaNew);
                _context.TAprobacion.Update(aprobacion);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }           
        }
    }
}
