using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class GoalMissionRepository : Repository<GoalMission>, IGoalMissionRepository
{
    private readonly CIDbContext _dbContext;
    public GoalMissionRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext; 
    }
}
