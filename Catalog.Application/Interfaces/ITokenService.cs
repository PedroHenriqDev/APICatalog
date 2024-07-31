using Catalog.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Catalog.Application.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, 
                                         IConfiguration configuration);

    Task<string> SaveRefreshTokenAsync(ApplicationUser user,
                                       IConfiguration configuration);
  

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token,
                                                 IConfiguration configuration);
}
