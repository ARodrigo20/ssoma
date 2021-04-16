using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.General.UsuarioRol.Queries.GetUsuarioRol;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.UsuarioRol.Command.UpdateUsuarioRol
{
    public class UpdateUsuarioRolCommand : IRequest<Unit>
    {
        public string CodPersona { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public bool TipoLogueo { get; set; }

        public ICollection<int> Roles { get; set; }

        public class UpdateUsuarioRolCommandHandler : IRequestHandler<UpdateUsuarioRolCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateUsuarioRolCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateUsuarioRolCommand request, CancellationToken cancellationToken)
            {
                var _usuario = _context.TUsuario.Include(p => p.UsuarioRoles).First(t => t.CodPersona.Equals(request.CodPersona));

                _usuario.Password = request.Password;
                _usuario.Usuario = request.Usuario;
                _usuario.TipoLogueo = request.TipoLogueo;

                _usuario.UsuarioRoles = _usuario.UsuarioRoles.Where(p => p.Estado == true).ToList();

                var interRoles = _usuario.UsuarioRoles.Select(x => x.CodRol).Intersect(request.Roles).ToList(); //update
                var leftRoles = request.Roles.Except(_usuario.UsuarioRoles.Select(x => x.CodRol)).ToList(); //new
                var rightRoles = _usuario.UsuarioRoles.Select(x => x.CodRol).Except(request.Roles).ToList(); //delete

                foreach (var CodRol in interRoles)
                {
                    var pm = _usuario.UsuarioRoles.First(t => t.CodRol.Equals(CodRol));
                    pm.Estado = true;
                }

                foreach (var CodRol in leftRoles)
                {
                    UsuarioRolDto usuariorol = new UsuarioRolDto();
                    usuariorol.CodRol = CodRol;
                    var _usuariorol = _mapper.Map<UsuarioRolDto, TUsuarioRol>(usuariorol);
                    _usuario.UsuarioRoles.Add(_usuariorol);
                }

                foreach (var CodRol in rightRoles)
                {
                    var pm = _usuario.UsuarioRoles.First(t => t.CodRol.Equals(CodRol));
                    pm.Estado = false;
                }

                _context.TUsuario.Update(_usuario);
                //_context.TUsuario.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
