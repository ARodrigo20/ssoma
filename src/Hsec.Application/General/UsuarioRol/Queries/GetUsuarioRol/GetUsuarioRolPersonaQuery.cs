using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Hsec.Domain.Enums;

namespace Hsec.Application.General.UsuarioRol.Queries.GetUsuarioRol
{
    public class GetUsuarioRolPersonaQuery : IRequest<UsuarioDto>
    {
        public string CodPersona{ get; set; }

        public class GetUsuarioRolPersonaHandler : IRequestHandler<GetUsuarioRolPersonaQuery, UsuarioDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public GetUsuarioRolPersonaHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = HttpContext;
            }

            public async Task<UsuarioDto> Handle(GetUsuarioRolPersonaQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var _usuario = _context.TUsuario.Where(t => t.CodPersona == request.CodPersona && t.Estado == true ).FirstOrDefault();

                    if(_usuario == null) throw new Exception("usuario no existe");

                    var _rol = _context.TUsuarioRol.Where(t => t.CodUsuario.Equals(_usuario.CodUsuario) && t.Estado == true).FirstOrDefault();

                    var usuario = new UsuarioDto();

                    usuario.CodUsuario = _usuario.CodUsuario.ToString();
                    usuario.Usuario = _usuario.Usuario;
                    usuario.CodPersona = _usuario.CodPersona;
                    usuario.TipoLogueo = _usuario.TipoLogueo;
                    usuario.TipoUsuario = TipoUsuario.Persona.ToString();
                    usuario.Rol = _rol.CodRol.ToString();

                    return usuario;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}