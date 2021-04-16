using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hsec.Application.General.Accesos.Queries.GetAccesos;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Models;
using Hsec.Application.General.Login.Commands.ChangePassword;
using Hsec.Application.General.Login.Commands.ChanguePassword;
using Hsec.Application.General.Login.Queries.GetLogin;
using Hsec.Application.General.Login.Queries.GetLoginQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hsec.WebApi.Controllers.General
{
    public class LoginController : ApiController
    {
        [AllowAnonymous]
        [Route("Authenticate")]
        [HttpPost]
        public async Task<UserVM> Authenticate(UserPassVM userLg) //Usuario = userLg.Usuario, Pasword= userLg.Password,Dominio=userLg.Dominio,Token=userLg.Token
        {
            return await Mediator.Send(new GetLoginQuery() { Usuario= userLg }).ConfigureAwait(false);
        }
        
        [Route("ChangePass")]
        [HttpPost]
        public async Task<ActionResult<Unit>> ChangePass(UserPassVM value)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var CodUsuario = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "primarysid").Value;            

            if (CodUsuario != value.codUsuario + "") throw new GeneralFailureException("Usuario no coincide con usuario del token");            
            return await Mediator.Send(new ChangePassCommand() { user = value }).ConfigureAwait(false);           
        }

    }
}