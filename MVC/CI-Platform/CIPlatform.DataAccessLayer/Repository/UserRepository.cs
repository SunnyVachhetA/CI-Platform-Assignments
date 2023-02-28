using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccessLayer.Repository;
public class UserRepository : Repository<User>, IUserRepository
{
    private readonly CIDbContext _dbContext;
    
    public UserRepository(CIDbContext db) : base(db)
    {
        _dbContext = db;
    }
}
