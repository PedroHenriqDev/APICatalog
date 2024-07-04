using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APICatalog.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, 
                                         IConfiguration configuration);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token,
                                                 IConfiguration configuration);
}
