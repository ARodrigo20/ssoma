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

namespace Hsec.Application.General.UsuarioRol.Commands.CreateUsuario
{
    public class CreateUsuarioCommand : IRequest<Unit>
    {

        public CreateUsuarioDto data { get; set; }

        public class CreateUsuarioRolCommandHandler : IRequestHandler<CreateUsuarioCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateUsuarioRolCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var existe = _context.TUsuario.Where(t => t.Usuario.Equals(request.data.usuario)).Count();
                    if (existe > 0) throw new Exception("existe usuario");

                    var codmax = nextCod(_context.TUsuario.Max(t => (t.CodUsuario)));
                    CreateUsuarioDto VMUsuarioRol = request.data;

                    VMUsuarioRol.codUsuario = null;
                    
                    var usuario = _mapper.Map<CreateUsuarioDto, TUsuario>(VMUsuarioRol);
                    usuario.CodUsuario = codmax;
                    var usuarioRol = new TUsuarioRol();

                    usuario.CodPersona = VMUsuarioRol.codPersona;
                    usuarioRol.CodRol = Int32.Parse(VMUsuarioRol.rol);
                    usuarioRol.CodUsuario = codmax;

                    _context.TUsuario.Add(usuario);
                    _context.TUsuarioRol.Add(usuarioRol);

                    await _context.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.ToString());
                    throw e;
                }
            }

            public int nextCod(int corrmax)
            {

                if (corrmax == null) corrmax = 1;
                else
                {
                    corrmax++;
                }
                return corrmax;
            }
        }
    }
}
