using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IUserRepository : IRepository<User>
{
    void UpdatePassword(string? email, string password);
    IEnumerable<User> FetchUserInformationWithMissionInvite();

    User ValidateUserCredentialRepo(UserLoginVM credential);
    Task<IEnumerable<string>> GetUserEmailList(Func<User, bool> filter);
}
