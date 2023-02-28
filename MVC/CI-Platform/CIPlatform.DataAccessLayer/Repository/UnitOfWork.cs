using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccessLayer.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly CIDbContext _dbContext;
    public UnitOfWork(CIDbContext dbContext)
    {
        _dbContext = dbContext;
        UserRepo = new UserRepository(_dbContext);
    }
    public IUserRepository UserRepo { get; private set; }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
