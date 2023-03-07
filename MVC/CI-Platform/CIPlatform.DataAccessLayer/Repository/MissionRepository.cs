using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class MissionRepository : Repository<Mission>, IMissionRepository
{
    private readonly CIDbContext _dbContext;
    public MissionRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Mission> GetAllMissions()
    {

        var result = _dbContext.Missions.Include(mission => mission.MissionMedia)
                    .Include(mission => mission.GoalMissions)
                    .Include(mission => mission.MissionApplications)
                    .Include(mission => mission.FavouriteMissions)
                    .Include(mission => mission.MissionSkills)
                    .Include(mission => mission.Theme)
                    .ToList();

        return result;
    }
}
