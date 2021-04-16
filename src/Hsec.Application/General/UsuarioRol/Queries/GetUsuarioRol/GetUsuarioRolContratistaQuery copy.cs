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
    public class GetUsuarioRolContratistaQuery : IRequest<UsuarioDto>
    {
        public string CodUsuario{ get; set; }

        public class GetUsuarioRolContratistaHandler : IRequestHandler<GetUsuarioRolContratistaQuery, UsuarioDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public GetUsuarioRolContratistaHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = HttpContext;
            }

            public async Task<UsuarioDto> Handle(GetUsuarioRolContratistaQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var codigo = Int32.Parse(request.CodUsuario);
                    var _usuario = _context.TUsuario.Where(t => t.CodUsuario == codigo && t.Estado == true ).FirstOrDefault();

                    if(_usuario == null) throw new Exception("usuario no existe");

                    var usuario = new UsuarioDto();

                    usuario.CodUsuario = _usuario.CodUsuario.ToString();
                    usuario.Usuario = _usuario.Usuario;
                    //usuario.Ruc = _usuario.CodPersona;
                    //usuario.TipoLogueo = _usuario.TipoLogueo;
                    usuario.TipoUsuario = TipoUsuario.Contratista.ToString();

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