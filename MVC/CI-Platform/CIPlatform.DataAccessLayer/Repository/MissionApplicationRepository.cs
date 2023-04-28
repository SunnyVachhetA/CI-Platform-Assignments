using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class MissionApplicationRepository : Repository<MissionApplication>, IMissionApplicationRepository
{
    private readonly CIDbContext _dbContext;
    public MissionApplicationRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<MissionApplication> FetchMissionApplicationInformation()
    {
        return _dbContext
            .MissionApplications
            .Include( application => application.User );
    }

    private IQueryable<MissionApplication> FetchMissionApplicationWithMissionInformation()
    {
        return _dbContext
            .MissionApplications
            .Include( application => application.Mission );
    }
    public IQueryable<MissionApplication> FetchRecentVolunteersInformation(long missionId)
    {
        var query = FetchMissionApplicationInformation()?.Where( application => application.MissionId == missionId );
        return query!;
    }

    public IEnumerable<MissionApplication> FetchSingleUserMissions(Func<MissionApplication, bool> filter)
    {
        var result = FetchMissionApplicationWithMissionInformation().Where( filter );
        return result;
    }

    public int DeleteMissionApplication(long missionId, long userId)
    {
        var query = "DELETE FROM mission_application WHERE mission_id = {0} AND user_id = {1}";

        return _dbContext.Database.ExecuteSqlRaw(query, missionId, userId);
    }

    public IEnumerable<MissionApplication> LoadAllApplications() => ApplicationWithUserAndMission();
    public int UpdateApplicationStatus(long id, byte status)
    {
        var query = "UPDATE mission_application SET approval_status = {0} WHERE mission_application_id = {1}";
        return _dbContext.Database.ExecuteSqlRaw(query, status, id);
    }

    public IEnumerable<MissionApplication> LoadApplications(Func<MissionApplication, bool> filter)
        =>
            dbSet
                .Include(app => app.User)
                .Include(app => app.Mission)
                .Where(filter)
                .OrderBy(app => app.ApprovalStatus);

    public IEnumerable<MissionApplication> FetchApplicationWithMission()
    {
        return
            dbSet
                .Include(app => app.Mission)
                .ToList();
    }

    private IEnumerable<MissionApplication> ApplicationWithUserAndMission() 
        => 
            dbSet
                .Include(app => app.User)
                .Include(app => app.Mission)
                .OrderBy(app => app.ApprovalStatus)
                .ToList();

}
