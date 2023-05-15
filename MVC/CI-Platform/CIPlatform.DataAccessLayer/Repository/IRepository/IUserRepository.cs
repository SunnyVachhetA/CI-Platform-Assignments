using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using System.Linq.Expressions;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IUserRepository : IRepository<User>
{
    void UpdatePassword(string? email, string password);
    IEnumerable<User> FetchUserInformationWithMissionInvite();

    User ValidateUserCredentialRepo(UserLoginVM credential);
    Task<IQueryable<string>> GetUserEmailList(Func<User, bool> filter);
    User FetchUserProfile(Func<User, bool> filter);
    void UpdateUserAvatar(string filePath, long userId);
    int UpdateUserStatus(long userId, byte status);
    void SetUserStatusToActive(string email);
    Admin CheckAdminCredential(string credentialEmail, string credentialPassword);
    int IsAdminEmail(string email);
    Task<List<User>> UserWithSettingsAsync(Expression<Func<User, bool>> filter);
    List<User> UserWithSettings(Expression<Func<User, bool>> filter);
}
