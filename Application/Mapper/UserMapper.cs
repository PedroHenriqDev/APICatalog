using Infrastructure.Domain;
using Communication.DTOs.Requests;

namespace Application.Mapper;

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
