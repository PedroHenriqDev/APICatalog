using Infrastructure.Domain;
using System.Security.Claims;

namespace Application.Interfaces;

public interface IUserClaimService
{
    void AddUserRolesToClaims(IList<string> roles, List<Claim> authClaims);

    List<Claim> CreateAuthClaims(ApplicationUser user);
}
