using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class VerifyEmailRepository: Repository<VerifyEmail>, IVerifyEmailRepository
{
    private readonly CIDbContext _dbContext;
    public VerifyEmailRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void DeleteActivationToken(string email)
    {
        var emailParam = new SqlParameter("@email", email);
        _dbContext.Database.ExecuteSqlRaw("DELETE verify_email WHERE email = @email", emailParam);
    }
}
