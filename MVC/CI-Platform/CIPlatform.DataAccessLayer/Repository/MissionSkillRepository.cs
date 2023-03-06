using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class MissionSkillRepository : Repository<MissionSkill>, IMissionSkillRepository
{
    private readonly CIDbContext _dbContext;
    public MissionSkillRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
