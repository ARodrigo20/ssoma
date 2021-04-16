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
using Microsoft.EntityFrameworkCore;

namespace Hsec.Application.General.UsuarioRol.Commands.UpdateRolPerfil
{
    public class UpdateRolPerfilCommand : IRequest<Unit>
    {
        public UpdateRolPerfilDto data { get; set; }

        public class UpdateRolPerfilRolCommandHandler : IRequestHandler<UpdateRolPerfilCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateRolPerfilRolCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateRolPerfilCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var data = request.data;
                    var rol = _context.TRol
                        .Include(t => t.RolAccesos)
                        .Where(t => t.Estado == true && t.CodRol == Int32.Parse(data.codRol))
                        .FirstOrDefault();
                        
                    if(rol == null){
                        throw new Exception("no existe rol");
                    }
                    
                    rol.Descripcion = request.data.descripcion;
                    rol.RolAccesos = new List<TRolAcceso>();

                    foreach (var item in data.permisos)
                    {
                        rol.RolAccesos.Add(new TRolAcceso(){CodAcceso = item});
                    }
                    _context.TRol.Update(rol);
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
