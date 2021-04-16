using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Modulos.Commands.UpdateModulo
{
    public partial class UpdateModuloCommand : IRequest
    {
        public string CodModulo { get; set; }
        public string CodModuloPadre { get; set; }
        public string Descripcion { get; set; }

        public class UpdateModuloCommandHandler : IRequestHandler<UpdateModuloCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateModuloCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateModuloCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TModulo.FindAsync(request.CodModulo);

                if (entity == null)
                {
                    throw new NotFoundException("modulo", request.CodModulo);
                }


                entity.CodModuloPadre = request.CodModuloPadre;
                entity.Descripcion = request.Descripcion;
               

                _context.TModulo.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);                

                return Unit.Value;
            }           
        }
    }
}
