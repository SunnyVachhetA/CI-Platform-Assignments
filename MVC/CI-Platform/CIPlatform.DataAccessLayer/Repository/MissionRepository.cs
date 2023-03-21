using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using Microsoft.Data.SqlClient;
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

        var result = FetchMissionInformation().ToList();

        return result;
    }

    //Fetching mission details by id
    public Mission FetchMissionDetailsById(long id)
    {
        var result = dbSet
                    .Where(mission => mission.MissionId == id)
                    .Include(mission => mission.MissionMedia)
                    .Include(mission => mission.GoalMissions)
                    .Include(mission => mission.MissionApplications)
                        .ThenInclude( missonApplication => missonApplication.User )
                    .Include(mission => mission.FavouriteMissions)
                    .Include(mission => mission.MissionSkills)
                            .ThenInclude(ms => ms.Skill)
                    .Include(mission => mission.MissionRatings)
                    .Include(mission => mission.MissionDocuments)
                    .Include(mission => mission.Comments)
                    .Include(mission => mission.Theme)
                    .Include(mission => mission.City)
                    .Include(mission => mission.Country)
                    .FirstOrDefault();
        
        return result!;
    }
    public IQueryable<Mission> FetchMissionInformation()
    {
        return _dbContext.Missions
                    .Include(mission => mission.MissionMedia)
                    .Include(mission => mission.GoalMissions)
                    .Include(mission => mission.MissionApplications)
                        .ThenInclude( missonApplication => missonApplication.User )
                    .Include(mission => mission.FavouriteMissions)
                    .Include(mission => mission.MissionSkills)
                        .ThenInclude(ms => ms.Skill)
                    .Include( mission => mission.MissionRatings )
                    .Include( mission => mission.MissionDocuments )
                    .Include(mission => mission.Comments)
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

    public void UpdateMissionRating(long missionId, byte avgRating)
    {
        var msnId = new SqlParameter("@missionId", missionId);
        var ratingParam = new SqlParameter("@rating", avgRating);
        var updateParam = new SqlParameter("@updatedAt", DateTimeOffset.Now);

        _dbContext.Database.ExecuteSqlRaw("UPDATE mission SET rating = @rating, updated_at = @updatedAt WHERE mission_id = @missionId", msnId, ratingParam, updateParam);
    }
}
