using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IUserService
{
    void Add(UserRegistrationVM user);
    IEnumerable<UserRegistrationVM> FetchAllUsers(bool isActiveFlag);
    bool IsEmailExists(string email);
    UserRegistrationVM UpdateUserPassword(string? email, string password);
    UserRegistrationVM ValidateUserCredential(UserLoginVM credential);
}
