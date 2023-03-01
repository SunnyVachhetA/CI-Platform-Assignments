using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using System.Linq.Expressions;

namespace CIPlatform.DataAccessLayer.Repository;
public class UserRepository : Repository<User>, IUserRepository
{
    private readonly CIDbContext _dbContext;
    
    public UserRepository(CIDbContext db) : base(db)
    {
        _dbContext = db;
    }

    public User ValidateUserCredentialRepo(UserLoginVM credential)
    {
        Func<User, bool> value = (user) =>
                        {
                            return (user.Email == credential.Email && user.Password.Equals(credential.Password));
                        };
        var result = _dbContext.Users.FirstOrDefault
            (
            value
            );
        return result;
    }
}
