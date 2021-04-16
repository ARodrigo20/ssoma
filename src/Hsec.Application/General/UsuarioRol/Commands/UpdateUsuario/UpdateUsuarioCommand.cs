using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.UsuarioRol.Commands.UpdateUsuario
{
    public class UpdateUsuarioCommand : IRequest<Unit>
    {
        public string codUsuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public bool tipoLogueo { get; set; }
        public string rol { get; set; }


        public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateUsuarioCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var _usuario = _context.TUsuario.First(t => t.CodUsuario == Int32.Parse(request.codUsuario) );

                    if (_usuario == null) throw new Exception("usuario no existe, operacion invalida");

                    var existe = _context.TUsuario.Where(t => t.Usuario.Equals(request.usuario)).Count();
                    if (existe > 0 && _usuario.Usuario != request.usuario) throw new Exception("existe usuario, pruebe otro nombre de usuario");

                    if (request.password != null && !request.password.Equals("")) {
                        _usuario.Password = request.password;
                    }
                    _usuario.TipoLogueo = request.tipoLogueo;
                    _usuario.Usuario = request.usuario;

                    var _usuario_rol = _context.TUsuarioRol.FirstOrDefault(t => t.CodUsuario == Int32.Parse(request.codUsuario));
                    var _usuario_rol_nuevo = new TUsuarioRol();

                    if (_usuario_rol == null) throw new Exception("rol no asociado ");

                    _usuario_rol_nuevo.CodRol = Int32.Parse(request.rol);
                    _usuario.UsuarioRoles.Remove(_usuario_rol);
                    _usuario.UsuarioRoles.Add(_usuario_rol_nuevo);

                    _context.TUsuario.Update(_usuario);
                    await _context.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
