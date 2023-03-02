using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IUserService
{
    void Add(UserRegistrationVM user);
    bool IsEmailExists(string email);
    UserRegistrationVM UpdateUserPassword(string? email, string password);
    UserRegistrationVM ValidateUserCredential(UserLoginVM credential);
}
