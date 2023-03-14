using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        var result = FetchMissionInformation().ToList();

        return result;
    }

    //Fetching mission details by id
    public Mission FetchMissionDetailsById(long id)
    {
        var result = _dbContext.Missions
                    .Include(mission => mission.MissionMedia)
                    .Include(mission => mission.GoalMissions)
                    .Include(mission => mission.MissionApplications)
                    .Include(mission => mission.FavouriteMissions)
                    .Include(mission => mission.MissionSkills)
                    .Include(mission => mission.Theme)
                    .Include(mission => mission.City)
                    .Include(mission => mission.Country)
                    .FirstOrDefault( mission=>mission.MissionId == id );
        
        return result!;
    }

    public List<Mission> FetchRelatedMissionsByTheme(int? themeId)
    {
        var missions = FetchMissionInformation();
        var result = missions.Where(mission => mission.ThemeId == themeId)?.ToList();
        return result!;
    }

    public IQueryable<Mission> FetchMissionInformation()
    {
        return _dbContext.Missions
                    .Include(mission => mission.MissionMedia)
                    .Include(mission => mission.GoalMissions)
                    .Include(mission => mission.MissionApplications)
                    .Include(mission => mission.FavouriteMissions)
                    .Include(mission => mission.MissionSkills)
                    .Include(mission => mission.Theme)
                    .Include(mission => mission.City)
                    .Include(mission => mission.Country);
    }

    //Db call for filtering out missions
    public List<Mission> LoadFilteredMissions(FilterModel filterModel)
    {
        var missions = FetchMissionInformation().AsQueryable();

        if(filterModel != null)
        {
            if( !string.IsNullOrEmpty(filterModel.SearchKeyword) )
            {
                missions = missions.Where( msn =>  msn.Theme.Title!.Contains(filterModel.SearchKeyword)  );
            }
        }

        return missions.ToList();
    }
}
