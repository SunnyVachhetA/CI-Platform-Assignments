using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IUserRepository : IRepository<User>
{
    void UpdatePassword(string? email, string password);
    User ValidateUserCredentialRepo(UserLoginVM credential);
}
