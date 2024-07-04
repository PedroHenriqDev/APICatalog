using Microsoft.AspNetCore.Identity;

namespace APICatalog.Domain;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
