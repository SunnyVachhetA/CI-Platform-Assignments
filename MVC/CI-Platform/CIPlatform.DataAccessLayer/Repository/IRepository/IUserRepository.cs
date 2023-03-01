using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IUserRepository : IRepository<User>
{
    User ValidateUserCredentialRepo(UserLoginVM credential);
}
