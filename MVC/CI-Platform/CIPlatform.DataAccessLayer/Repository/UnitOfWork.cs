using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;

namespace CIPlatform.DataAccessLayer.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly CIDbContext _dbContext;
    public UnitOfWork(CIDbContext dbContext)
    {
        _dbContext = dbContext;
        UserRepo = new UserRepository(_dbContext);
        PasswordResetRepo = new PasswordResetRepository(_dbContext);
    }
    public IUserRepository UserRepo { get; private set; }
    public IPasswordResetRepository PasswordResetRepo { get; private set; }
    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
