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

    private IEnumerable<User> FetchUserDetails( Func<User, bool> filter)
    {
        var query =
            dbSet
                .Include(user => user.UserSkills)
                    .ThenInclude(skill => skill.Skill)
                .Where(filter);
        return query;
    }
    public User FetchUserProfile(Func<User, bool> filter)
    {
        var user = FetchUserDetails(filter)?.FirstOrDefault() ?? null!;

        return user;
    }

    public void UpdateUserAvatar(string filePath, long userId)
    {
        var avatarParam = new SqlParameter("@avatar", filePath);
        var userIdParam = new SqlParameter("@userId", userId);

        _dbContext.Database.ExecuteSqlRaw("UPDATE [user] SET avatar = @avatar WHERE user_id = @userId", avatarParam, userIdParam);

    }

    public int UpdateUserStatus(long userId, byte status)
    {
        var statusParam = new SqlParameter("@status", status);
        var userIdParam = new SqlParameter("@userId", userId);

        return _dbContext.Database.ExecuteSqlRaw("UPDATE [user] SET status = @status WHERE user_id = @userId", statusParam, userIdParam);
    }

    public void SetUserStatusToActive(string email)
    {
        var statusParam = new SqlParameter("@status", 1);
        var emailParam = new SqlParameter("@email", email);

        _dbContext.Database.ExecuteSqlRaw("UPDATE [user] SET status = @status WHERE email = @email", statusParam, emailParam);
    }

    public Admin CheckAdminCredential(string credentialEmail, string credentialPassword)
    {
        var query = "SELECT * FROM admin WHERE email = @Email AND password = @Password";
        var emailParameter = new SqlParameter("@Email", credentialEmail);
        var passwordParameter = new SqlParameter("@Password", credentialPassword);

        var admin = _dbContext.Admins.FromSqlRaw(query, emailParameter, passwordParameter).FirstOrDefault();
        return admin!;
    }

    public int IsAdminEmail(string email)
    {
        var query = "SELECT COUNT(*) FROM admin WHERE email = {0}";
        return _dbContext.Database.ExecuteSqlRaw(query, email);
    }
}
