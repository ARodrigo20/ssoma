using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Hsec.Application.Common.Exceptions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;

namespace Hsec.Application.General.UsuarioRol.Commands.CreateRolPerfil
{
    public class CreateRolPerfilCommand : IRequest<Unit>
    {
        public CreateRolPerfilDto data { get; set; }

        public class CreateRolPerfilRolCommandHandler : IRequestHandler<CreateRolPerfilCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateRolPerfilRolCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CreateRolPerfilCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var data = request.data;
                    var rol = new TRol();
                    rol.Descripcion = request.data.descripcion;
                    // rol.RolAccesos = new List<TRolAcceso>();

                    foreach (var item in data.permisos)
                    {
                        rol.RolAccesos.Add(new TRolAcceso(){CodAcceso = item});
                    }
                    _context.TRol.Add(rol);
                    await _context.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.ToString());
                    throw e;
                }
            }

        }
    }
}
