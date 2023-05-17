using CISkillMaster.Entities.DTO;

namespace CISkillMaster.Services.Abstract;

public interface IUserService
{
    Task<UserInformationDTO?> SignInUser(UserLoginDTO credential);
}
