using Catalog.Infrastructure.Identity;
using System.Security.Claims;

namespace Catalog.Application.Interfaces;

public interface IUserClaimService
{
    void AddUserRolesToClaims(IList<string> roles, List<Claim> authClaims);

    List<Claim> CreateAuthClaims(ApplicationUser user);
}
