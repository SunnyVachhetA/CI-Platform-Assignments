using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IUserService
{
    void Add(UserRegistrationVM user);
    IEnumerable<UserRegistrationVM> FetchAllUsers(bool isActiveFlag);
    Task<string> GetUserName(long userId);
    bool IsEmailExists(string email);
    void SendUserMissionInviteService(IEnumerable<string> userEmailList, string senderUserName, string missionInviteLink, IEmailService _emailService);
    UserRegistrationVM UpdateUserPassword(string? email, string password);
    UserRegistrationVM ValidateUserCredential(UserLoginVM credential);
    Task<IEnumerable<string>> GetUserEmailList(long[] userId);
}
