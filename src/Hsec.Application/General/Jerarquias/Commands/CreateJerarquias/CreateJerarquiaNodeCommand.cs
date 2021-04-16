using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Jerarquias.Commands.CreateJerarquias
{
    public class CreateJerarquiaNodeCommand : IRequest<Unit>
    {
        public int? NodeParent { get; set; }  
        public string Description { get; set; }
        public string Tipo { get; set; }
        public string CodElipse { get; set; }

        public class CreateJerarquiaNodeCommandHandler : IRequestHandler<CreateJerarquiaNodeCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreateJerarquiaNodeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateJerarquiaNodeCommand request, CancellationToken cancellationToken)
            {
                if (request.Tipo == "G" || request.Tipo == "S" || request.Tipo == "O")
                {
                    var entity = new TJerarquia();
                    var JerarquiaPadre = await _context.TJerarquia.FirstOrDefaultAsync(u => u.CodPosicion == request.NodeParent).ConfigureAwait(true);
                    if (JerarquiaPadre != null)
                    {                        
                        entity.CodPosicion = nextCod();
                        entity.CodPosicionPadre = JerarquiaPadre.CodPosicion;
                        entity.Descripcion = request.Description;
                        entity.CodElipse = request.CodElipse;
                        entity.PathJerarquia = JerarquiaPadre.PathJerarquia + entity.CodPosicion.ToString() + "/";
                        entity.NivelJerarquia = (short)(JerarquiaPadre.PathJerarquia.Split('/').Length - 1);
                        entity.Tipo = request.Tipo;
                        entity.Estado = true;

                        _context.TJerarquia.Add(entity);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    else new GeneralFailureException("Nodo padre no fue encontrado");
                }

                else {
                    new TipoJerarquiaException();             
                }
                return Unit.Value;
            }
            public int? nextCod()
            {
                int? maxAge = _context.TJerarquia.Max(t => t.CodPosicion) + 1;
                return maxAge;
            }
        }
    }
}
