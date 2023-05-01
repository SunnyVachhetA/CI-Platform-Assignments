using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IUserRepository : IRepository<User>
{
    void UpdatePassword(string? email, string password);
    IEnumerable<User> FetchUserInformationWithMissionInvite();

    User ValidateUserCredentialRepo(UserLoginVM credential);
    Task<IEnumerable<string>> GetUserEmailList(Func<User, bool> filter);
    User FetchUserProfile(Func<User, bool> filter);
    void UpdateUserAvatar(string filePath, long userId);
    int UpdateUserStatus(long userId, byte status);
    void SetUserStatusToActive(string email);
    Admin CheckAdminCredential(string credentialEmail, string credentialPassword);
    int IsAdminEmail(string email);
}
