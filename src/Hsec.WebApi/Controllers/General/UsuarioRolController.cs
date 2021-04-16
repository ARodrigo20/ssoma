using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MediatR;
using Hsec.Application.General.UsuarioRol.Command.UpdateUsuarioRol;
using Hsec.Application.General.UsuarioRol.Queries.GetUsuarioRol;
using Hsec.Application.General.UsuarioRol.Commands.CreateUsuario;
using Hsec.Application.General.UsuarioRol.Commands.UpdateUsuario;
using Hsec.Application.General.UsuarioRol.Queries.SearchUsuario;
using Hsec.Application.General.Roles.Queries.GetRoles;
using Hsec.Application.General.Roles.Queries.GetRolPerfil;
using Hsec.Application.General.Roles.Queries.GetPermisos;
using Hsec.Application.General.UsuarioRol.Commands.CreateRolPerfil;
using Hsec.Application.General.UsuarioRol.Commands.UpdateRolPerfil;

namespace Hsec.WebApi.Controllers.General
{
    public class UsuarioRolController : ApiController
    {
        [HttpPost("createUsuario")]
        public async Task<ActionResult<Unit>> CreateUsuario([FromBody] CreateUsuarioCommand value)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;
            if (rol.Equals("1"))
            {
                return await Mediator.Send(value);
            }
            else{return BadRequest();}
            // return await Mediator.Send(value);
        }
        [HttpPost("createUsuarioContratista")]
        public async Task<ActionResult<Unit>> CreateUsuarioContratista([FromBody] CreateUsuarioContratistaCommand value)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;
            if (rol.Equals("1"))
            {
                return await Mediator.Send(value);
            }
            else{return BadRequest();}
            // return await Mediator.Send(value);
        }

        [HttpPost("searchUsuarioContratista")]
        public async Task<ActionResult<UsuarioVM>> Post([FromBody] SearchUsuarioContratistaQuery value)
        {
            return await Mediator.Send(value);
        }

        [HttpPost("updateUsuario")]
        public async Task<ActionResult<Unit>> updateUsuario([FromBody] UpdateUsuarioCommand value)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;
            if (rol.Equals("1"))
            {
                return await Mediator.Send(value);
            }
            else{return BadRequest();}
            // return await Mediator.Send(value);
        }

        [HttpPost("updateUsuarioContratista")]
        public async Task<ActionResult<Unit>> updateUsuarioContratista([FromBody] UpdateUsuarioContratistaCommand value)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;
            if (rol.Equals("1"))
            {
                return await Mediator.Send(value);
            }
            else{return BadRequest();}
            // return await Mediator.Send(value);
        }

        [HttpGet("getUsuarioPersonaRol/{codigo}")]
        public async Task<ActionResult<UsuarioDto>> PostPersona(string codigo)
        {
            return await Mediator.Send(new GetUsuarioRolPersonaQuery()
            {
                CodPersona = codigo
            });
        }

        [HttpGet("getUsuarioContratistaRol/{codigo}")]
        public async Task<ActionResult<UsuarioDto>> PostContratista(string codigo)
        {
            return await Mediator.Send(new GetUsuarioRolContratistaQuery()
            {
                CodUsuario = codigo
            });
        }
        
        [HttpPost("updateUsuarioRol")]
        public async Task<ActionResult<Unit>> Post([FromBody] UpdateUsuarioRolCommand value)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;
            if (rol.Equals("1"))
            {
                return await Mediator.Send(value);
            }
            else{return BadRequest();}
            // return await Mediator.Send(value);
        }
        [HttpGet("Rol")]
        public async Task<ActionResult<RolVM>> GetRol()
        {
            return await Mediator.Send(new GetRolesQuery(){});
        }

        [HttpGet("RolPerfil/{codigo}")]
        public async Task<ActionResult<GetRolPerfilVM>> GetOneRol(string codigo)
        {
            return await Mediator.Send(new GetRolPerfilQuery() { CodRol = codigo });
        }

        [HttpPost("RolPerfil")]
        public async Task<ActionResult<Unit>> CreateOneRol(CreateRolPerfilDto data)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;
            if (rol.Equals("1"))
            {
                return await Mediator.Send(new CreateRolPerfilCommand() {data = data});
            }
            else{return BadRequest();}
        }

        [HttpPost("RolPerfil/{codigo}")]
        public async Task<ActionResult<Unit>> UpdateOneRol(string codigo,UpdateRolPerfilDto data)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;
            if (rol.Equals("1"))
            {
                return await Mediator.Send(new UpdateRolPerfilCommand() {data = data});
            }
            else{return BadRequest();}
        }

        [HttpGet("Permisos")]
        public async Task<ActionResult<PermisosVM>> GetPermiso()
        {
            return await Mediator.Send(new GetPermisosQuery() {});
        }
    }
}
