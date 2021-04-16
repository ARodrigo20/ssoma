using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.UsuarioRol.Commands.UpdateUsuario
{
    public class UpdateUsuarioContratistaCommand : IRequest<Unit>
    {
        public int CodUsuario { get; set; }

        public string Password { get; set; }

        public class UpdateUsuarioContratistaCommandHandler : IRequestHandler<UpdateUsuarioContratistaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateUsuarioContratistaCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateUsuarioContratistaCommand request, CancellationToken cancellationToken)
            {

                var _usuario = _context.TUsuario.First(t => t.CodUsuario == request.CodUsuario);

                if (_usuario == null) throw new Exception("no existe usuario");

                if (request.Password.Equals("")) throw new Exception("password no valido");

                _usuario.Password = request.Password;

                _context.TUsuario.Update(_usuario);
                //_context.TUsuario.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
