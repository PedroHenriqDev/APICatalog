using Infrastructure.Domain;
using Application.DTOs;

namespace Application.Mapper;

static public class UserMapper
{
    public static ApplicationUser MapToApplicatioUser(this RegisterModelDTO registerModelDTO) 
    {
        return new ApplicationUser
        {
            UserName = registerModelDTO.UserName,
            Email = registerModelDTO.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
    }
}
