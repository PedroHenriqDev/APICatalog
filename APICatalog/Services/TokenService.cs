using APICatalog.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APICatalog.Services;

public class TokenService : ITokenService
{
    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var privateKey = GetPrivateKey(configuration);

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey),
                                                        SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
            Audience = configuration.GetSection("JWT").GetValue<string>("ValidAudience"),
            Issuer = configuration.GetSection("JWT").GetValue<string>("ValidIssuer"),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return token;
    }

    public string GenerateRefreshToken()
    {
        var randombytes = new byte[128];

        using var randomNumberGenerator = RandomNumberGenerator.Create();

        randomNumberGenerator.GetBytes(randombytes);

        var refreshToken = Convert.ToBase64String(randombytes);

        return refreshToken;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration configuration)
    {
        var privateKey = GetPrivateKey(configuration);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(privateKey)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                                                   out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                               StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private byte[] GetPrivateKey(IConfiguration configuration)
    {
        var key = configuration.GetSection("JWT").GetValue<string>("SecretKey") ??
               throw new InvalidOperationException("Invalid secret key");

        var privateKey = Encoding.UTF8.GetBytes(key);

        return privateKey;
    }
}
