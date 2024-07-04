using APICatalog.Domain;
using APICatalog.DTOs;

namespace APICatalog.Mapper;

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
