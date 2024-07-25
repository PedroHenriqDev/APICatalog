using Infrastructure.Domain;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Tokens;

public class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public TokenService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

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

    public async Task<string> SaveRefreshTokenAsync(ApplicationUser user, IConfiguration configuration)
    {
        var refreshToken = GenerateRefreshToken();

        _ = int.TryParse(configuration["JWT:RefreshTokenValidityInMinutes"],
            out int refreshTokenValidityInMinutes);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

        await _userManager.UpdateAsync(user);

        return user.RefreshToken;
    }

    private byte[] GetPrivateKey(IConfiguration configuration)
    {
        var key = configuration.GetSection("JWT").GetValue<string>("SecretKey") ??
               throw new InvalidOperationException("Invalid secret key");

        var privateKey = Encoding.UTF8.GetBytes(key);

        return privateKey;
    }

    private string GenerateRefreshToken()
    {
        var randombytes = new byte[128];

        using var randomNumberGenerator = RandomNumberGenerator.Create();

        randomNumberGenerator.GetBytes(randombytes);

        var refreshToken = Convert.ToBase64String(randombytes);

        return refreshToken;
    }

}
