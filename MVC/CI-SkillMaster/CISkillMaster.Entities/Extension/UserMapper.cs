using CISkillMaster.Entities.DataModels;
using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Enum;

namespace CISkillMaster.Entities.Extension;

public static class UserMapper
{
    public static UserInformationDTO ToUserInformationDTO(this User user)
    {
        UserInformationDTO dto = new()
        {
            UserName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Id = user.Id,
            Role = user.Role == 0 ? UserRole.User : UserRole.Admin,
            Status = (UserStatus) user.Status
        };
        return dto;
    }
}
