using Catalog.Infrastructure.Identity;
using Catalog.Communication.DTOs.Requests;

namespace Catalog.Application.Mapper;

static public class UserMapper
{
    public static ApplicationUser MapToApplicatioUser(this RequestUserDTO requestUser) 
    {
        return new ApplicationUser
        {
            UserName = requestUser.UserName,
            Email = requestUser.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
    }
}
