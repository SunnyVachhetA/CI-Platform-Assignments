using CISkillMaster.DataAccessLayer.Abstract;
using CISkillMaster.DataAccessLayer.Data;
using CISkillMaster.Entities.DataModels;

namespace CISkillMaster.DataAccessLayer.Implementation;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}
