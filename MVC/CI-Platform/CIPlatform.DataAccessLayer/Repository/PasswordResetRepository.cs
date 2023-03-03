using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class PasswordResetRepository : Repository<PasswordReset>, IPasswordResetRepository
{
    private readonly CIDbContext _dbContext;
    public PasswordResetRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}
