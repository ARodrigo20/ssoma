using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.General.Login.Commands.ChangePassword;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Login.Commands.ChanguePassword
{
    public class ChangePassCommand : IRequest<Unit>
    {
        public UserPassVM user { get; set; }

        public class ChangePassCommandHandler : IRequestHandler<ChangePassCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public ChangePassCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ChangePassCommand request, CancellationToken cancellationToken)
            {                               
                var User = await _context.TUsuario.FirstOrDefaultAsync(u => u.CodUsuario == request.user.codUsuario).ConfigureAwait(true);

                if (User == null) throw new NotFoundException(nameof(Login), request.user.codUsuario);

                if (User.Estado) {
                    if (User.Password == request.user.password && request.user.password != request.user.newPassword) {
                        User.Password = request.user.newPassword;
                        _context.TUsuario.Update(User);
                        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(true);
                    }
                    else throw new GeneralFailureException("Credenciales incorrectas");
                }
                else throw new UpdateEstadoRegistroException(nameof(Login), User.Estado);

                return Unit.Value;
            }
            
        }
    }
}

