using CISkillMaster.Entities.Enum;

namespace CISkillMaster.Entities.DTO;

public class UserInformationDTO
{
    public string UserName { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }  
}
