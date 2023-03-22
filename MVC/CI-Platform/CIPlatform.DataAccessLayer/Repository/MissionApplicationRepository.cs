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

    public IQueryable<MissionApplication> FetchRecentVolunteersInformation(long missionId)
    {
        var query = FetchMissionApplicationInformation()?.Where( application => application.MissionId == missionId );
        return query!;
    }
}
