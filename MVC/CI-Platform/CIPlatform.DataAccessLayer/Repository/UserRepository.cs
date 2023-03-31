using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class UserRepository : Repository<User>, IUserRepository
{
    private readonly CIDbContext _dbContext;
    
    public UserRepository(CIDbContext db) : base(db)
    {
        _dbContext = db;
    }

    public IEnumerable<User> FetchUserInformationWithMissionInvite()
    {
        var query = dbSet
                    .Include(user => user.MissionInviteFromUsers);
        return query;
    }
    public void UpdatePassword(string? email, string password)
    {
        var user = new SqlParameter("@email", email);
        var passwordParam = new SqlParameter("@password", password);

        _dbContext.Database.ExecuteSqlRaw("UPDATE [user] SET password = @password WHERE email = @email", user, passwordParam);
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

    public Task<IEnumerable<string>> GetUserEmailList(Func<User, bool> filter)
    {
          return Task.Run(  () =>     
              dbSet
              .Where(filter)?
              .Select(user => user.Email.ToLower())
          )!;
    }
}
