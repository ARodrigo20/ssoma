using System.Collections.Generic;
using System.Security.Claims;

namespace Hsec.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string issuer, string key, int minutes, IEnumerable<Claim> claims);
    }
}
