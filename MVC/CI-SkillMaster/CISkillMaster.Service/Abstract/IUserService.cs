using CISkillMaster.Entities.DataModels;
using CISkillMaster.Entities.DTO;

namespace CISkillMaster.Services.Abstract;

public interface IUserService: IService<User>
{
    Task<UserInformationDTO?> SignInUser(UserLoginDTO credential);
}
