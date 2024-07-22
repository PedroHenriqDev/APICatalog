using Infrastructure.Domain;
using Application.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Services;

public class UserClaimService : IUserClaimService
{
    public void AddUserRolesToClaims(IList<string> roles, List<Claim> authClaims) 
    {
        if (authClaims is null)
            throw new ArgumentNullException("Claims null");

        authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
    }

    public List<Claim> CreateAuthClaims(ApplicationUser user) 
    {
        if(user is null) 
            throw new ArgumentNullException("User null");


        return new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
    }
}
