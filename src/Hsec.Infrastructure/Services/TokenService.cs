using Hsec.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hsec.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(string issuer, string key, int minutes, IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(issuer,
                key,
                claims,
                expires: DateTime.Now.AddMinutes(minutes),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
