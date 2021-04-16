using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Hsec.Application.Common.Interfaces;
using System.Collections.Generic;
using Hsec.Application.General.Login.Queries.GetLogin;
using Hsec.Application.Common.Exceptions;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Hsec.Domain.Entities.General;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using Hsec.Application.General.Login.Commands.ChangePassword;
using System.Security.Cryptography;
using System.DirectoryServices.AccountManagement;

namespace Hsec.Application.General.Login.Queries.GetLoginQuery
{
    public class GetLoginQuery : IRequest<UserVM>
    {
        public UserPassVM Usuario { get; set; }      
        public class GetLoginQueryHandler : IRequestHandler<GetLoginQuery, UserVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ITokenService _tokenService;
            private List<TAcceso> AccesosUser;
            public IConfiguration Configuration { get; }
            public GetLoginQueryHandler(IApplicationDbContext context, IMapper mapper, ITokenService tokenService, IConfiguration configuration)
            {
                _context = context;
                _mapper = mapper;
                _tokenService = tokenService;
                Configuration = configuration;
            }

            public async Task<UserVM> Handle(GetLoginQuery request, CancellationToken cancellationToken)
            {
                var user = await _context.TUsuario.Include(i => i.UsuarioRoles).FirstOrDefaultAsync(f => f.Usuario == request.Usuario.usuario).ConfigureAwait(true);               
                if (user == null) throw new GeneralFailureException("Usuario no Existe");
                //if (user.UsuarioRoles.Any(r => r.CodRol==6)) throw new GeneralFailureException("Sistema en mantenimiento espere su acceso");
                bool tipoB = !user.TipoLogueo && request.Usuario.password == user.Password;
                bool tipoW = user.TipoLogueo && LoginActiveDirectory(request.Usuario.usuario, request.Usuario.password, "anyaccess");
                if (user.Estado && (tipoB || tipoW))
                {
                    var claims = new List<Claim>() {
                        //new Claim("isAdministrator", "true"),
                        new Claim(ClaimTypes.PrimarySid, ""+user.CodUsuario),
                        new Claim(ClaimTypes.Name, user.Usuario),
                        new Claim(ClaimTypes.Role, user.UsuarioRoles.FirstOrDefault().CodRol+""),
                        new Claim(ClaimTypes.NameIdentifier, user.CodPersona)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Issuer = Configuration["JWT:Issuer"],
                        Audience = Configuration["JWT:Audience"],
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddMinutes(int.Parse(Configuration["JWT:TokenDuration"])),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenRide = tokenHandler.CreateToken(tokenDescriptor);

                    var token = tokenHandler.WriteToken(tokenRide);
                    //this._tokenService.GenerateToken(Configuration["JWT:Issuer"], Configuration["JWT:Key"], int.Parse(Configuration["JWT:TokenDuration"]), claims);

                    var persona = await _context.TPersona.FirstOrDefaultAsync(w => w.CodPersona == user.CodPersona).ConfigureAwait(true);
                    //if (request.Usuario.tipoPersona == null) request.Usuario.tipoPersona = 1;
                    if (request.Usuario.tipoPersona > 0 && request.Usuario.tipoPersona != Int32.Parse(persona.CodTipoPersona)) throw new GeneralFailureException("Usuario no Existe");
                    int[] roles = user.UsuarioRoles.Where(x => x.Estado).Select(x => x.CodRol).ToArray();
                    var rolesDes = _context.TRol.Where(r => roles.Contains(r.CodRol)).Select(a => a.Descripcion).ToList();
                    //var Accesos = await _context.TAcceso.Join(_context.TRolAcceso,  )
                    var Accesos = _context.TAcceso.Include(i => i.Hijos).Include(i => i.RolAccesos).Where(a => a.Estado).ToList();//.Where(a => a.RolAccesos.Select(x => x.CodRol).Intersect(roles).Any()).ToList();
                    this.AccesosUser = Accesos.Where(a => a.RolAccesos.Where(x => x.Estado).Select(x => x.CodRol).Intersect(roles).Any()).ToList();

                    var BarMenu = new List<INavData>();
                    foreach (var item in this.AccesosUser)
                    {
                        if (item.Padre == null)
                        {
                            INavData node = recursion(item);
                            BarMenu.Add(node);
                        }
                    }

                    var model = new UserVM();



                    model.token = token;
                    model.usuario = user.Usuario;
                    model.codUsuario = user.CodUsuario;
                    model.codPersona = user.CodPersona;
                    model.rol = string.Join(";", rolesDes);
                    model.codRol= string.Join(";", roles);
                    model.barMenu = BarMenu;
                    if (persona != null)
                    {
                        model.nombres = persona.Nombres;
                        model.apellidos = persona.ApellidoPaterno + " " + persona.ApellidoMaterno;
                        model.nroDocumento = persona.NroDocumento;
                        model.cargo = persona.Ocupacion;
                        model.sexo = persona.Sexo.ToString();
                        model.codTipoPersona = Int32.Parse(persona.CodTipoPersona);
                        model.empresa = persona.Empresa;
                        model.email = persona.Email;
                    }
                    else {
                        model.codTipoPersona = 2;
                    }
                    return model;
                }
                else
                {
                    if (!user.Estado) throw new GeneralFailureException("Usuario Inactivo");
                    else throw new GeneralFailureException("Contraseña incorrecta");
                }

                
            }

            public static bool LoginActiveDirectory(string user, string password, string domain)
            {             
                try
                {
                    bool isValid = false;
                    PrincipalContext insPrincipalContext = new PrincipalContext(ContextType.Domain, domain);
                    isValid = insPrincipalContext.ValidateCredentials(user, password, ContextOptions.Negotiate);                   
                    return isValid;
                }
                catch (Exception ex)
                {                   
                    throw new Exception("ErrorValidateRed"+ ex.Message);
                }

            }

            public INavData recursion(TAcceso it)
            {
                var nodo = new INavData();
                nodo.name = it.Nombre;
                nodo.url = it.Componente;
                nodo.icon = it.Icono;
                if (it.BadgeText != null || it.BadgeVariant != null) {
                    nodo.badge = new INavBadge(it.BadgeVariant,it.BadgeText);
                }

                if (it.Hijos.Count > 0)
                {
                    //foreach (var item in it.Hijos)
                    var hijos = this.AccesosUser.Where(a => a.CodPadre == it.CodAcceso).ToList();
                    foreach (var item in hijos)
                    {
                        nodo.children.Add(recursion(item));
                    }
                }
                else nodo.children = null;
                return nodo;
            }
        }
    }
}
        




