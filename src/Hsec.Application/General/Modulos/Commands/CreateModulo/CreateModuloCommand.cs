using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Modulos.Commands.CreateModulo
{
    public partial class CreateModuloCommand : IRequest<int>
    {
        public string CodModulo { get; set; }
        public string CodModuloPadre { get; set; }
        public string Descripcion { get; set; }

        public class CreateModuloCommandHandler : IRequestHandler<CreateModuloCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateModuloCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateModuloCommand request, CancellationToken cancellationToken)
            {
                var entity = new TModulo();

                entity.CodModulo = request.CodModulo;

                _context.TModulo.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return int.Parse(entity.CodModulo);
            }
        }
    }
}
    

